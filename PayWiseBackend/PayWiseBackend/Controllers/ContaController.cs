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
        public ActionResult<RetrieveContaDTO> PegarPorId(int id)
        {
            var conta = _context.Contas.Find(id);

            if (conta is null)
                return NotFound(new { message = "Conta não encontrada" });

            var contaResponse = _mapper.Map<RetrieveContaDTO>(conta);

            return Ok(new { contaResponse });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CriarConta(int clienteId, CreateContaDTO novaConta)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Id == clienteId);
            if (cliente is null)
            {
                return NotFound(new { message = "Cliente não encontrado" });
            }

            if (cliente.temConta)
            {
                return BadRequest(new { message = "O cliente já possui uma conta" });
            }

            var contaCadastrar = _mapper.Map<Conta>(novaConta);

            var result = _context.Contas.Add(contaCadastrar);
            var contaCadastrada = result.Entity;
            cliente.Conta = contaCadastrada;
            cliente.temConta = true;
            _context.SaveChanges();

            return CreatedAtAction(nameof(PegarPorId), new { contaCadastrada.Id }, contaCadastrada);
        }

    }
}
