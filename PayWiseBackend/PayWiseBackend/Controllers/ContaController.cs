using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;
using PayWiseBackend.Infra.Services;

namespace PayWiseBackend.Controllers;

[Route("/contas")]
[ApiController]
public class ContaController : ControllerBase
{
    private readonly PaywiseDbContextSqlite _contextSqlite;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly IClienteService _clienteService;
    private readonly IContaService _contaService;

    public ContaController(
        PaywiseDbContextSqlite contextSqlite, 
        IMapper mapper, 
        IAuthService authService,
        IClienteService clienteService,
        IContaService contaService
        )
    {
        _contextSqlite = contextSqlite;
        _mapper = mapper;
        _authService = authService;
        _clienteService = clienteService;
        _contaService = contaService;
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RetrieveContaDTO>> PegarPorId()
    {
        string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

        int? contaId = _authService.GetContaIdFromAccessToken(accessToken);

        var conta = await _contaService.BuscarContaPorId(contaId);

        if (conta is null)
            return NotFound(new { message = "Cliente não possui conta." });

        if (!conta.EstaAtiva)
            return NotFound(new { message = "Cliente não possui conta." });

        var contaResponse = _mapper.Map<RetrieveContaDTO>(conta);

        return Ok(new { conta = contaResponse });
    }

    [Authorize]
    [HttpPost("criar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CriarConta(CreateContaDTO novaConta)
    {
        string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

        int? clienteId = _authService.GetClienteIdFromToken(accessToken);

        var cliente = await _clienteService.BuscarClientePorId(clienteId);

        if(cliente is null)
            return NotFound(new { message = "Cliente não encontrada(o)." });

        if (cliente.TemConta)
            return BadRequest(new { message = "Cliente já possui uma conta." });

        var contaResponse = await _contaService.CadastrarConta(cliente.Id, novaConta);

        string newAccessToken = _authService.GenerateAccessToken(cliente.Id, contaResponse.Id);

        return CreatedAtAction(nameof(PegarPorId), new { message = "Conta criada.", conta = contaResponse, accessToken = newAccessToken});
    }

    [HttpPut("alterar/limites")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RetrieveContaLimitesDTO>> AlterarLimites(UpdateContaLimitesDTO novoLimite)
    {
        string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

        int? contaId = _authService.GetContaIdFromAccessToken(accessToken);

        var conta = await _contaService.BuscarContaPorId(contaId);

        if (conta is null)
            return BadRequest(new { message = "Cliente não possui conta." });

        if(!conta.EstaAtiva)
            return BadRequest(new { message = "Cliente não possui conta." });

        if (conta.Pin != novoLimite.Pin)
            return BadRequest(new { message = "Senha PIN inválida." });

        var contaResponse = await _contaService.AlterarLimitesConta(conta, novoLimite);

        return Ok(new { limites = contaResponse });

    }

    [HttpDelete]
    public async Task<IActionResult> DeletarConta()
    {
        string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

        int? clienteId = _authService.GetClienteIdFromToken(accessToken);
        int? contaId = _authService.GetContaIdFromAccessToken(accessToken);

        var cliente = await _clienteService.BuscarClientePorId(clienteId);
        if (cliente is null)
            return NotFound(new { message = "Cliente não encontrada(o)." });

        if (!cliente.TemConta)
            return NotFound(new { message = "Cliente não possui conta." });

        var conta = await _contaService.BuscarContaPorId(contaId);

        if (conta is null)
            return BadRequest(new { message = "Cliente não possui conta." });

        if (conta.Saldo > 0)
            return BadRequest(new { message = "Não é possível desativar conta com saldo." });

        await _contaService.DeleteConta(cliente, conta);

        return Ok(new { message = "Conta desativada." });
    }

    [Authorize]
    [HttpGet("saldo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<double>> ConsultarSaldo()
    {
        string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

        int? contaId = _authService.GetContaIdFromAccessToken(accessToken);

        var conta = await _contaService.BuscarContaPorId(contaId);

        if (conta is null)
            return BadRequest(new { message = "Conta não existe." });

        if (!conta.EstaAtiva)
            return BadRequest(new { message = "Conta não existe." });

        decimal saldo = conta.Saldo;

        return Ok(new { saldo });
    }

    [Authorize]
    [HttpPut("sacar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Sacar(CreateTransacaoSaqueDTO dadosTransacao)
    {
        string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

        int? contaId = _authService.GetContaIdFromAccessToken(accessToken);

        Conta? conta = await _contaService.BuscarContaPorId(contaId);

        if (conta is null)
            return BadRequest(new { message = "Conta não existe" });

        if(!conta.EstaAtiva)
            return BadRequest(new { message = "Conta não existe" });

        if (conta.Saldo <= 0 || conta.Saldo < dadosTransacao.Valor)
            return BadRequest(new { message = "Saldo insuficiente" });

        if (conta.Pin != dadosTransacao.Pin)
            return BadRequest(new { message = "Senha PIN inválida" });

        await _contaService.Sacar(conta, dadosTransacao);

        decimal saldo = conta.Saldo;

        return Ok(new { saldo });
    }

    [Authorize]
    [HttpPut("depositar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Depositar(CreateTransacaoDepositoDTO dadosTransacao)
    {
        string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

        int? contaId = _authService.GetContaIdFromAccessToken(accessToken);
        var conta = await _contaService.BuscarContaPorId(contaId);

        if (conta is null)
            return BadRequest(new { message = "Conta não existe" });

        if(!conta.EstaAtiva)
            return BadRequest(new { message = "Conta não existe" });

        await _contaService.Depositar(conta, dadosTransacao);

        decimal saldo = conta.Saldo;

        return Ok(new { saldo });
    }

    [Authorize]
    [HttpPut("transferir")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Transferir(CreateTransacaoTransferenciaDTO dadosTransacao)
    {
        string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

        int? contaId = _authService.GetContaIdFromAccessToken(accessToken);
        var conta = await _contaService.BuscarContaPorId(contaId);

        if (conta is null)
            return BadRequest(new { message = "Conta não existe" });

        if(!conta.EstaAtiva)
            return BadRequest(new { message = "Conta não existe" });

        if (conta.Saldo <= 0 || conta.Saldo < dadosTransacao.Valor)
            return BadRequest(new { mesage = "Saldo insuficiente" });

        var contaDestino = await _contaService.BuscarContaPorNumero(dadosTransacao.ContaDestino);

        if (contaDestino is null)
            return BadRequest(new { message = "Conta de destino inexistente" });

        if(!contaDestino.EstaAtiva)
            return BadRequest(new { message = "Conta de destino inexistente" });

        await _contaService.Transferencia(conta, contaDestino, dadosTransacao);

        decimal saldo = conta.Saldo;

        return Ok(new { saldo });
    }

    [Authorize]
    [HttpGet("historico")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PegarHistorico(
        [FromQuery(Name = "from")] DateTime? from,
        [FromQuery(Name = "to")] DateTime? to
        )
    {
        string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

        int? contaId = _authService.GetContaIdFromAccessToken(accessToken);

        var historicoResponse = await _contaService.BuscarHistoricoDaConta(contaId, from, to);

        return Ok(new { historico = historicoResponse });
    }

}
