using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.Models;
using PayWiseBackend.Services;

namespace PayWiseBackend.Controllers;

[ApiController]
[Route("/historico")]
public class HistoricoController : ControllerBase
{

    private readonly PaywiseDbContext _context;
    private readonly IAuthService _service;

    public HistoricoController(PaywiseDbContext context, IAuthService service)
    {
        _context = context;
        _service = service;
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PegarHistorico()
    {
        string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        if (accessToken is null)
            return Unauthorized(new { message = "Cliente não autorizado." });

        int? contaId = _service.GetContaIdFromAccessToken(accessToken);
        var conta = await _context.Contas.FindAsync(contaId);
        if (conta is null)
            return BadRequest(new { message = "Conta não existe." });

        List<Transacao> transacoes = await _context.Transacoes.Where(t => t.HistoricoId == conta.HistoricoId).ToListAsync();
       
        return Ok(new { transacoes });
    }
}
