using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;
using PayWiseBackend.Infra.Services;

namespace PayWiseBackend.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IClienteService _clienteService;
    private readonly IMapper _mapper;
    private readonly IContaService _contaService;

    public AuthController(
        IAuthService authService,
        IClienteService clienteService,
        IMapper mapper,
        IContaService contaService
        )
    {
        _authService = authService;
        _clienteService = clienteService;
        _mapper = mapper;
        _contaService = contaService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDTO))]
    public async Task<ActionResult<AuthResponseDTO>> Autenticar(CreateLoginDTO loginCredentials)
    {
        Cliente? cliente = await _authService.ValidateCredentials(loginCredentials);
        if (cliente is null)
            return NotFound(new { message = "Credenciais inválidas." });

        string accessToken = _authService.GenerateAccessToken(cliente.Id, cliente.TemConta ? cliente.Conta.Id : null);
        string refreshToken = _authService.GenerateRefreshToken(cliente.Id, cliente.TemConta ? cliente.Conta.Id : null);

        await _authService.SalvarSessao(cliente.Id, refreshToken);

        Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.Now.AddHours(1)
        });

        AuthResponseDTO authResponse = new() { AccessToken = accessToken };

        return Ok(authResponse);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponseDTO))]
    public async Task<ActionResult<AuthResponseDTO>> RefrescarToken()
    {
        string? refreshToken = Request.Cookies["RefreshToken"];

        if (refreshToken is null)
            return Unauthorized(new { message = "Cliente não autorizada(o)." });

        int? clienteId = _authService.GetClienteIdFromToken(refreshToken);

        Cliente? cliente = await _clienteService.BuscarClientePorId(clienteId);

        if (cliente is null)
            return Unauthorized(new { message = "Cliente não existe." });

        string novoAccessToken;

        if (cliente.TemConta)
        {
            novoAccessToken = _authService.GenerateAccessToken(cliente.Id, cliente.Conta.Id);
        } else
        {
            novoAccessToken = _authService.GenerateAccessToken(cliente.Id, null);
        }

        AuthResponseDTO authResponse = new() { AccessToken = novoAccessToken };

        return Ok(authResponse);
    }

}
