using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly PaywiseDbContext _context;
        private readonly IMapper _mapper;

        public ClienteController(PaywiseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public IActionResult PegarPorId(int id)
        {
            var buscaCliente = _context.Clientes.Find(id);

            if (buscaCliente == null)
                return NotFound();

            return Ok(new { buscaCliente, buscaCliente.Conta});
        }

        [HttpPost]
        public IActionResult Cadastrar(CreateClientDTO novoCliente)
        {
            var clienteCadastrar = _mapper.Map<Cliente>(novoCliente);

            var result = _context.Clientes.Add(clienteCadastrar);
            _context.SaveChanges();

            var clienteSalvo = result.Entity;

            return CreatedAtAction("PegarPorId", new { clienteSalvo.Id }, clienteSalvo);
        }
    }
}
