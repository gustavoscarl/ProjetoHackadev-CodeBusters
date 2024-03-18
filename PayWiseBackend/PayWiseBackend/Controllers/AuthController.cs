using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;
using PayWiseBackend.Services;

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
    public async Task<IActionResult> Autenticar(LoginRequestDTO loginCredentials)
    {
        Cliente? cliente = await _service.ValidateCredentials(loginCredentials);
        if (cliente is null)
            return BadRequest(new { message = "Cliente não existe." });

        string accessToken = _service.GenerateAccessToken(cliente.Id, cliente.TemConta ? cliente.ContaId : null);
        string refreshToken = _service.GenerateRefreshToken(cliente.Id, cliente.TemConta ? cliente.ContaId : null);

        Sessao sessao = new Sessao()
        {
            RefreshToken = refreshToken
        };

        var sessaoResult = await _context.Sessoes.AddAsync(sessao);
        cliente.Sessao = sessaoResult.Entity;
        await _context.SaveChangesAsync();

        Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });

        return Ok(new { accessToken });
    }
}
