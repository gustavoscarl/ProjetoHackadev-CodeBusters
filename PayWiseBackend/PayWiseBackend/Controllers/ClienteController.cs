using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;
using PayWiseBackend.Services;

namespace PayWiseBackend.Controllers;

[Route("/clientes")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly PaywiseDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuthService _service;

    public ClienteController(PaywiseDbContext context, IMapper mapper, IAuthService service)
    {
        _context = context;
        _mapper = mapper;
        _service = service;
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RetrieveClienteDTO>> PegarPorId()
    {
        string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (accessToken is null)
            return Unauthorized(new { message = "Cliente não autorizado." });

        int? clienteId = _service.GetClienteIdFromAccessToken(accessToken);
        var buscaCliente = await _context.Clientes.FindAsync(clienteId);

        if (buscaCliente is null)
            return NotFound(new { message = "Cliente não encontrada(o)" });

        var clienteResponse = _mapper.Map<RetrieveClienteDTO>(buscaCliente);

        return Ok(new { clienteResponse });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Cadastrar([FromBody] CreateClientDTO novoCliente)
    {

        bool doesClientAlreadyExist = await _context.Clientes.AnyAsync(cliente => cliente.Cpf ==  novoCliente.Cpf || cliente.Rg == novoCliente.Rg);

        if (doesClientAlreadyExist)
            return Conflict(new { message = "Credenciais já cadastradas." });

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(novoCliente.Senha);
        novoCliente.Senha = hashedPassword;

        var clienteCadastrar = _mapper.Map<Cliente>(novoCliente);

        var result = _context.Clientes.Add(clienteCadastrar);
        await _context.SaveChangesAsync();

        var clienteSalvo = result.Entity;

        return CreatedAtAction("PegarPorId", new { clienteSalvo.Id }, new { message = "Cliente cadastrada(o)."});
    }
}
