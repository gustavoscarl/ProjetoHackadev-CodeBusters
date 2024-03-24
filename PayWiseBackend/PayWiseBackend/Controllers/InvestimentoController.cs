using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Infra.Services;

namespace PayWiseBackend.Controllers;

[Route("contas/investimentos")]
[ApiController]
public class InvestimentoController : ControllerBase
{

    private readonly IAuthService _authService;
    private readonly IInvestimentoService _investimentoService;
    private readonly IContaService _contaService;

    public InvestimentoController(
        IAuthService authService, 
        IInvestimentoService investimentoService,
        IContaService contaService)
    {
        _authService = authService;
        _investimentoService = investimentoService;
        _contaService = contaService;
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDTO))]
    public async Task<ActionResult<RetrieveInvestimentoDTO>> PegarPorId()
    {
        string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

        int? contaId = _authService.GetContaIdFromAccessToken(accessToken);

        if (!contaId.HasValue)
            return NotFound(new { message = "Cliente não possui conta." });

        var investimento = await _investimentoService.BuscarInvestimento(contaId);

        if (investimento is null)
            return NotFound(new { message = "Cliente não possui investimento." });

        return Ok(investimento);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDTO))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponseDTO))]
    public async Task<ActionResult<RetrieveInvestimentoDTO>> CriarInvestimento(CreateInvestimentoDTO novoInvestimento)
    {
        string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        var contaId = _authService.GetContaIdFromAccessToken(accessToken);

        if (contaId is null)
            return NotFound(new { message = "Cliente não possui conta." });

        if (novoInvestimento.Valor < 100)
            return BadRequest(new { message = "O investimento mínimo é de R$ 100.00." });

        novoInvestimento.Tempo = novoInvestimento.Tempo.Date;
        var proximoMes = DateTime.Now.AddMonths(1).Date;

        if (novoInvestimento.Tempo < proximoMes)
            return BadRequest(new { message = "Investimento deve ser de pelo menos um mês." });

        var conta = await _contaService.BuscarContaPorId(contaId);

        if (conta.Saldo < novoInvestimento.Valor)
            return BadRequest(new { message = "Conta com saldo insuficiente." });

        var investimentoResponse = await _investimentoService.CriarInvestimento(conta, novoInvestimento);
        return CreatedAtAction(nameof(PegarPorId), investimentoResponse);

    }
}
