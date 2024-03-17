using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Controllers;

[Route("/clientes")]
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

    [Authorize]
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<RetrieveClienteDTO> PegarPorId(int id)
    {
        var buscaCliente = _context.Clientes.Find(id);

        if (buscaCliente is null)
            return NotFound(new { message = "Cliente não encontrado" });

        var clienteResponse = _mapper.Map<RetrieveClienteDTO>(buscaCliente);

        return Ok(new { clienteResponse });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult Cadastrar([FromBody] CreateClientDTO novoCliente)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(novoCliente.Senha);
        novoCliente.Senha = hashedPassword;

        var clienteCadastrar = _mapper.Map<Cliente>(novoCliente);

        var result = _context.Clientes.Add(clienteCadastrar);
        _context.SaveChanges();

        var clienteSalvo = result.Entity;

        return CreatedAtAction("PegarPorId", new { clienteSalvo.Id }, clienteSalvo);
    }
}
