using AutoMapper;
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
    private readonly IAuthService _service;


    public AuthController(PaywiseDbContext context, IMapper mapper, IAuthService service)
    {
        _context = context;
        _mapper = mapper;
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Autenticar(CreateLoginDTO loginCredentials)
    {
        Cliente? cliente = await _service.ValidateCredentials(loginCredentials);
        if (cliente is null)
            return BadRequest(new { message = "Cliente não existe." });

        string accessToken = _service.GenerateAccessToken(cliente.Id, cliente.TemConta ? cliente.Conta.Id : null);
        string refreshToken = _service.GenerateRefreshToken(cliente.Id, cliente.TemConta ? cliente.Conta.Id : null);

        await _service.SalvarSessao(cliente.Id, refreshToken);

        Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });

        return Ok(new { accessToken });
    }
}
