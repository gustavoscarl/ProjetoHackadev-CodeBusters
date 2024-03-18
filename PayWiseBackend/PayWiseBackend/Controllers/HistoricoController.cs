using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;
using PayWiseBackend.Services;

namespace PayWiseBackend.Controllers;

[ApiController]
[Route("/historico")]
public class HistoricoController : ControllerBase
{

    private readonly PaywiseDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuthService _service;

    public HistoricoController(PaywiseDbContext context, IMapper mapper, IAuthService service)
    {
        _context = context;
        _mapper = mapper;
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
        var historico = await _context.Historicos
            .Include(h => h.Transacoes)
            .FirstOrDefaultAsync(c => c.Id == contaId);

        if (historico is null)
            return BadRequest(new { message = "Conta ou histórico inexistente." });

        var historicoResponse = _mapper.Map<RetrieveHistoricoDTO>(historico);

        return Ok(new { historico =  historicoResponse });
    }
}
