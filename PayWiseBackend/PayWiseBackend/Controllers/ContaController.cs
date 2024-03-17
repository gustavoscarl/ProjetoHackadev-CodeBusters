using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Enum;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Controllers
{
    [Route("/contas")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly PaywiseDbContext _context;
        private readonly IMapper _mapper;

        public ContaController(PaywiseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RetrieveContaDTO>> PegarPorId(int id)
        {
            var conta = await _context.Contas.FindAsync(id);

            if (conta is null)
                return NotFound(new { message = "Conta não encontrada" });

            var contaResponse = _mapper.Map<RetrieveContaDTO>(conta);

            return Ok(new { contaResponse });
        }

        [HttpPost("criar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CriarConta(int clienteId, CreateContaDTO novaConta)
        {
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


            await _context.Historicos.AddAsync(historico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PegarPorId), new { contaCadastrada.Id }, contaCadastrada);
        }

        [HttpGet("saldo{contaId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<double>> ConsultarSaldo(int contaId)
        {
            Conta? conta = await _context.Contas.FindAsync(contaId);

            if (conta is null)
                return BadRequest(new { message = "Conta não existe." });

            double saldo = conta.Saldo;
            return Ok(new { saldo });
        }

        [HttpPut("sacar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Sacar(int contaId, CreateTransacaoSaqueDTO dadosTransacao)
        {
            var conta = await _context.Contas.FindAsync(contaId);

            if (conta is null)
                return BadRequest(new { message = "Conta não existe" });

            if (conta.Saldo <= 0)
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
                HistoricoId = conta.HistoricoId
            };
            
            await _context.Transacoes.AddAsync(transacao);

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("depositar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Depositar(int contaId, CreateTransacaoDepositoDTO dadosTransacao)
        {
            var conta = await _context.Contas.FindAsync(contaId);

            if (conta is null)
                return BadRequest(new { message = "Conta não existe" });

            conta.Saldo += dadosTransacao.Valor;

            Transacao transacao = new Transacao()
            {
                Descricao = dadosTransacao.Descricao ?? string.Empty,
                Horario = new DateTime(),
                Tipo = TransacaoTipo.DEPOSITO,
                Valor = dadosTransacao.Valor,
                HistoricoId = conta.HistoricoId
            };

            await _context.Transacoes.AddAsync(transacao);

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("transferir")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Transferir(int contaId, CreateTransacaoTransferenciaDTO dadosTransacao)
        {
            var conta = await _context.Contas.FindAsync(contaId);

            if (conta is null)
                return BadRequest(new { message = "Conta não existe" });

            if (conta.Saldo <= 0)
                return BadRequest(new { mesage = "Saldo insuficiente" });

            var contaDestino = await _context.Contas.FirstOrDefaultAsync(c => c.Numero == dadosTransacao.ContaDestino);

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
                HistoricoId = conta.HistoricoId
            };

            await _context.Transacoes.AddAsync(transacao);

            Transacao transacaoDestino = new Transacao()
            {
                Descricao = dadosTransacao.Descricao ?? string.Empty,
                Horario = new DateTime(),
                Tipo = TransacaoTipo.TRANSFERENCIA,
                Valor = dadosTransacao.Valor,
                HistoricoId = contaDestino.HistoricoId
            };

            await _context.Transacoes.AddAsync(transacaoDestino);

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
