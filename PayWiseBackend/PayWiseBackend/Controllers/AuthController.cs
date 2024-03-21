using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;
using PayWiseBackend.Infra.Services;

namespace PayWiseBackend.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : Controller
{
    private readonly PaywiseDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly IClienteService _clienteService;
    private readonly IContaService _contaService;

    public AuthController(
        PaywiseDbContext context, 
        IMapper mapper, 
        IAuthService authService,
        IClienteService clienteService,
        IContaService contaService
        )
    {
        _context = context;
        _mapper = mapper;
        _authService = authService;
        _clienteService = clienteService;
        _contaService = contaService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Autenticar(CreateLoginDTO loginCredentials)
    {
        Cliente? cliente = await _authService.ValidateCredentials(loginCredentials);
        if (cliente is null)
            return BadRequest(new { message = "Cliente não existe." });

        string accessToken = _authService.GenerateAccessToken(cliente.Id, cliente.TemConta ? cliente.Conta.Id : null);
        string refreshToken = _authService.GenerateRefreshToken(cliente.Id, cliente.TemConta ? cliente.Conta.Id : null);

        await _authService.SalvarSessao(cliente.Id, refreshToken);

        Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });

        return Ok(new { accessToken });
    }

    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RefrescarToken()
    {
        string? refreshToken = Request.Cookies["RefreshToken"];

        if (refreshToken is null)
            return Unauthorized(new { message = "Cliente não autorizada(o)." });

        int? clienteId = _authService.GetClienteIdFromToken(refreshToken);

        Cliente? cliente = await _clienteService.BuscarClientePorId(clienteId);

        if (cliente is null)
            return BadRequest(new { message = "Cliente não existe." });

        string novoAccessToken;

        if (cliente.TemConta)
        {
            novoAccessToken = _authService.GenerateAccessToken(cliente.Id, cliente.Conta.Id);
        } else
        {
            novoAccessToken = _authService.GenerateAccessToken(cliente.Id, null);
        }

        return Ok(new { accessToken = novoAccessToken });
    }

}
