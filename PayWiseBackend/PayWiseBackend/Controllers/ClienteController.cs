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
    private readonly IMapper _mapper;
    private readonly IClienteService _clienteService;
    private readonly IAuthService _authService;

    public ClienteController(
        IMapper mapper,
        IAuthService authService,
        IClienteService clienteService
        )
    {
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
        string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

        int? id = _authService.GetClienteIdFromToken(accessToken);

        var cliente = await _clienteService.BuscarClientePorId(id);

        if (cliente is null)
            return NotFound(new { message = "Cliente não encontrada(o)." });

        var clienteResponse = _mapper.Map<RetrieveClienteDTO>(cliente);

        return Ok(new { clienteResponse });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Cadastrar([FromBody] CreateClientDTO novoCliente)
    {

        bool doesClientAlreadyExist = await _clienteService.CheckClienteCredentials(novoCliente.Cpf, novoCliente.Rg);

        if (doesClientAlreadyExist)
            return Conflict(new { message = "Credenciais já cadastradas." });

        var clienteSalvo = await _clienteService.CadastrarCliente(novoCliente);

        return CreatedAtAction(nameof(PegarPorId), new { message = "Cliente cadastrada(o).", cliente = clienteSalvo});
    }
}
