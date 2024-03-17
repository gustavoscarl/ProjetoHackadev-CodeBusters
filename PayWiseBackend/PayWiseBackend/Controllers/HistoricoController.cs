using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Controllers;

[ApiController]
[Route("/historico")]
public class HistoricoController : ControllerBase
{

    private readonly PaywiseDbContext _context;
    private readonly IMapper _mapper;

    public HistoricoController(PaywiseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet("{contaId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PegarHistorico(int contaId)
    {
        var conta = await _context.Contas.FindAsync(contaId);
        if (conta is null)
            return BadRequest(new { message = "Conta não existe." });

        List<Transacao> transacoes = await _context.Transacoes.Where(t => t.HistoricoId == conta.HistoricoId).ToListAsync();
       
        return Ok(new { transacoes });
    }
}
