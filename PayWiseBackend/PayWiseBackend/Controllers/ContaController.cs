using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Controllers
{
    [Route("[controller]")]
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
            {
                return NotFound(new { message = "Cliente não encontrado" });
            }

            if (cliente.TemConta)
            {
                return BadRequest(new { message = "O cliente já possui uma conta" });
            }

            var contaCadastrar = _mapper.Map<Conta>(novaConta);

            var result = await _context.Contas.AddAsync(contaCadastrar);
            var contaCadastrada = result.Entity;
            cliente.Conta = contaCadastrada;
            cliente.TemConta = true;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PegarPorId), new { contaCadastrada.Id }, contaCadastrada);
        }

        [HttpPut("sacar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Sacar(int contaId, double valor)
        {
            var conta = await _context.Contas.FindAsync(contaId);

            if (conta is null)
                return BadRequest(new { message = "Conta não existe" });

            if (conta.Saldo <= 0)
                return BadRequest(new { message = "Saldo insuficiente" });

            conta.Saldo -= valor;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("depositar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Depositar(int contaId, double valor)
        {
            var conta = await _context.Contas.FindAsync(contaId);

            if (conta is null)
                return BadRequest(new { message = "Conta não existe" });

            conta.Saldo += valor;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("transferir")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Transferir(int contaId, int numeroContaDestino, double valor)
        {
            var conta = await _context.Contas.FindAsync(contaId);

            if (conta is null)
                return BadRequest(new { message = "Conta não existe" });

            if (conta.Saldo <= 0)
                return BadRequest(new { mesage = "Saldo insuficiente" });

            var contaDestino = await _context.Contas.FirstOrDefaultAsync(c => c.Numero == numeroContaDestino);

            if (contaDestino is null)
                return BadRequest(new { message = "Conta de destino inexistente" });

            conta.Saldo -= valor;
            contaDestino.Saldo += valor;

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
