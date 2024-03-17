using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PayWiseBackend.Domain.Context;

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

    /*[HttpGet("{contaId:int}")]
    public async Task<IActionResult> PegarHistorico(int contaId)
    {
        var conta = await _context.Contas.FindAsync(contaId);
       
        return Ok(new { historico = conta.Historico });
    }*/
}
