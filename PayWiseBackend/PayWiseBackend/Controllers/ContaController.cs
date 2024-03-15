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

        [HttpPost]
        public IActionResult CriarConta(int clienteId, CreateContaDTO novaConta)
        {
            // Verificar se o cliente existe
            Cliente cliente = _context.Clientes.FirstOrDefault(c => c.Id == clienteId);
            if (cliente == null)
            {
                return NotFound("Cliente não encontrado");
            }

            // Verificar se o cliente já possui uma conta
            if (cliente.Conta != null)
            {
                return BadRequest("O cliente já possui uma conta");
            }

            // Mapear os dados da nova conta para o objeto Conta
            var contaCadastrar = _mapper.Map<Conta>(novaConta);

            // Associar a nova conta ao cliente
            contaCadastrar.ClienteId = clienteId;
            cliente.Conta = contaCadastrar; // Associar a nova conta ao cliente

            // Adicionar a nova conta ao contexto do banco de dados
            _context.Contas.Add(contaCadastrar);

            // Salvar as mudanças no banco de dados
            _context.SaveChanges();

            return Ok("Conta criada com sucesso");
        }

    }
}
