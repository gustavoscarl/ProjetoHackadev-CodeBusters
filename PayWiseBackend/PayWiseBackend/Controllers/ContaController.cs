using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Enum;
using PayWiseBackend.Domain.Models;
using PayWiseBackend.Infra.Services;

namespace PayWiseBackend.Controllers
{
    [Route("/contas")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly PaywiseDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _service;

        public ContaController(PaywiseDbContext context, IMapper mapper, IAuthService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RetrieveContaDTO>> PegarPorId()
        {
            string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (accessToken is null)
                return Unauthorized(new { message = "Cliente não autorizado." });

            int? contaId = _service.GetContaIdFromAccessToken(accessToken);
            var conta = await _context.Contas.FindAsync(contaId);

            if (conta is null)
                return NotFound(new { message = "Conta não encontrada" });

            var contaResponse = _mapper.Map<RetrieveContaDTO>(conta);

            return Ok(new { contaResponse });
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
            if (accessToken is null)
                return Unauthorized(new { message = "Cliente não autorizado." });

            int? clienteId = _service.GetClienteIdFromAccessToken(accessToken);
            if (clienteId is null)
                return Unauthorized(new { message = "Cliente não autorizado." });

            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == clienteId);

            if (cliente is null)
                return NotFound(new { message = "Cliente não encontrado" });

            if (cliente.TemConta)
                return BadRequest(new { message = "O cliente já possui uma conta" });

            int numConta = int.Parse(await _context.Contas.MaxAsync(conta => conta.Numero) ?? "0000");
            numConta += 1;
            Conta contaCadastrar = new Conta()
            {
                Numero = numConta.ToString("D6"),
                Pin = novaConta.Pin
            };

            Historico historico = new Historico();
            contaCadastrar.Historico = historico;

            var result = await _context.Contas.AddAsync(contaCadastrar);
            var contaCadastrada = result.Entity;
            cliente.Conta = contaCadastrada;
            cliente.TemConta = true;

            await _context.SaveChangesAsync();

            string novoAccessToken = _service.GenerateAccessToken(cliente.Id, contaCadastrada.Id);

            return CreatedAtAction(nameof(PegarPorId), new { contaCadastrada.Id }, new { message = "Conta criada.", accessToken = novoAccessToken});
        }

        [Authorize]
        [HttpGet("saldo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<double>> ConsultarSaldo()
        {
            string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (accessToken is null)
                return Unauthorized(new { message = "Cliente não autorizado." });

            int? contaId = _service.GetContaIdFromAccessToken(accessToken);
            Conta? conta = await _context.Contas.FindAsync(contaId);

            if (conta is null)
                return BadRequest(new { message = "Conta não existe." });

            double saldo = conta.Saldo;
            return Ok(new { saldo });
        }

        [Authorize]
        [HttpPut("sacar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Sacar(CreateTransacaoSaqueDTO dadosTransacao)
        {
            string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (accessToken is null)
                return Unauthorized(new { message = "Cliente não autorizado." });

            int? contaId = _service.GetContaIdFromAccessToken(accessToken);
            var conta = await _context.Contas.Include(c => c.Historico).FirstOrDefaultAsync(c => c.Id == contaId);

            if (conta is null)
                return BadRequest(new { message = "Conta não existe" });

            if (conta.Saldo <= 0 || conta.Saldo < dadosTransacao.Valor)
                return BadRequest(new { message = "Saldo insuficiente" });

            if (conta.Pin != dadosTransacao.Pin)
                return BadRequest(new { message = "Senha PIN inválida" });

            conta.Saldo -= dadosTransacao.Valor;

            Transacao transacao = new Transacao()
            {
                Descricao = dadosTransacao.Descricao ?? string.Empty,
                Horario = new DateTime(),
                Tipo = TransacaoTipo.SAQUE,
                Valor = dadosTransacao.Valor,
            };

            conta.Historico.Transacoes.Add(transacao);

            await _context.SaveChangesAsync();
            return Ok(new { saldo = conta.Saldo });
        }

        [Authorize]
        [HttpPut("depositar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Depositar(CreateTransacaoDepositoDTO dadosTransacao)
        {
            string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (accessToken is null)
                return Unauthorized(new { message = "Cliente não autorizado." });

            int? contaId = _service.GetContaIdFromAccessToken(accessToken);
            var conta = await _context.Contas.Include(c => c.Historico).FirstOrDefaultAsync(c => c.Id == contaId);

            if (conta is null)
                return BadRequest(new { message = "Conta não existe" });

            conta.Saldo += dadosTransacao.Valor;

            Transacao transacao = new Transacao()
            {
                Descricao = dadosTransacao.Descricao ?? string.Empty,
                Horario = DateTime.Now,
                Tipo = TransacaoTipo.DEPOSITO,
                Valor = dadosTransacao.Valor,
            };

            conta.Historico.Transacoes.Add(transacao);

            await _context.SaveChangesAsync();
            return Ok(new { saldo = conta.Saldo });
        }

        [Authorize]
        [HttpPut("transferir")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Transferir(CreateTransacaoTransferenciaDTO dadosTransacao)
        {
            string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (accessToken is null)
                return Unauthorized(new { message = "Cliente não autorizado." });

            int? contaId = _service.GetContaIdFromAccessToken(accessToken);
            var conta = await _context.Contas.Include(c => c.Historico).FirstOrDefaultAsync(c => c.Id == contaId);

            if (conta is null)
                return BadRequest(new { message = "Conta não existe" });

            if (conta.Saldo <= 0 || conta.Saldo < dadosTransacao.Valor)
                return BadRequest(new { mesage = "Saldo insuficiente" });

            var contaDestino = await _context.Contas.Include(c => c.Historico).FirstOrDefaultAsync(c => c.Numero == dadosTransacao.ContaDestino);

            if (contaDestino is null)
                return BadRequest(new { message = "Conta de destino inexistente" });

            conta.Saldo -= dadosTransacao.Valor;
            contaDestino.Saldo += dadosTransacao.Valor;

            Transacao transacao = new Transacao()
            {
                Descricao = dadosTransacao.Descricao ?? string.Empty,
                Horario = new DateTime(),
                Tipo = TransacaoTipo.TRANSFERENCIA,
                Valor = dadosTransacao.Valor,
            };

            conta.Historico.Transacoes.Add(transacao);

            Transacao transacaoDestino = new Transacao()
            {
                Descricao = dadosTransacao.Descricao ?? string.Empty,
                Horario = new DateTime(),
                Tipo = TransacaoTipo.TRANSFERENCIA,
                Valor = dadosTransacao.Valor,
            };

            contaDestino.Historico.Transacoes.Add(transacaoDestino);

            await _context.SaveChangesAsync();

            var saldo = conta.Saldo;

            return Ok(new { saldo });
        }

    }
}
