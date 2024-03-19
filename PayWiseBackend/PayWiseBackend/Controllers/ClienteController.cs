using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;
using PayWiseBackend.Infra.Services;

namespace PayWiseBackend.Controllers;

[Route("/clientes")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly PaywiseDbContext _context;
    private readonly IMapper _mapper;
    private readonly IClienteService _clienteService;
    private readonly IAuthService _authService;

    public ClienteController(
        PaywiseDbContext context,
        IMapper mapper,
        IAuthService authService,
        IClienteService clienteService
        )
    {
        _context = context;
        _mapper = mapper;
        _authService = authService;
        _clienteService = clienteService;
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

        int? clienteId = _authService.GetClienteIdFromAccessToken(accessToken);

        if (clienteId is null)
            return NotFound(new { message = "Cliente não encontrada(o)." });

        var clienteResponse = await _clienteService.BuscarClientePorId(clienteId);

        if (clienteResponse is null)
            return NotFound(new { message = "Cliente não encontrada(o)." });

        return Ok(new { clienteResponse });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Cadastrar([FromBody] CreateClientDTO novoCliente)
    {

        bool doesClientAlreadyExist = await _clienteService.CheckClienteCredentials(novoCliente.Cpf, novoCliente.Rg);

        if (doesClientAlreadyExist)
            return Conflict(new { message = "Credenciais já cadastradas." });

        string senhaHash = _authService.HashPassword(novoCliente.Senha);
        novoCliente.Senha = senhaHash;

        var clienteSalvo = await _clienteService.CadastrarCliente(novoCliente);

        return CreatedAtAction(nameof(PegarPorId), new { clienteSalvo.Id }, new { message = "Cliente cadastrada(o)."});
    }
}
