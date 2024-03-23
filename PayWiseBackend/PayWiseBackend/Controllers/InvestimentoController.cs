using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Infra.Services;

namespace PayWiseBackend.Controllers;

[Route("contas/investimentos")]
[ApiController]
public class InvestimentoController : ControllerBase
{

    private readonly IAuthService _authService;
    private readonly IInvestimentoService _investimentoService;

    public InvestimentoController(
        IAuthService authService, 
        IInvestimentoService investimentoService
        )
    {
        _authService = authService;
        _investimentoService = investimentoService;
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDTO))]
    public async Task<ActionResult<RetrieveInvestimentoDTO>> PegarPorId()
    {
        string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

        int? contaId = _authService.GetContaIdFromAccessToken(accessToken);

        if (!contaId.HasValue)
            return NotFound(new { message = "Cliente não possui conta." });

        var investimento = await _investimentoService.BuscarInvestimento(contaId);

        if (investimento is null)
            return NotFound(new { message = "Cliente não possui investimento." });

        return Ok(investimento);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDTO))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponseDTO))]
    public async Task<ActionResult<RetrieveInvestimentoDTO>> CriarInvestimento(CreateInvestimentoDTO novoInvestimento)
    {
        string? accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        var contaId = _authService.GetContaIdFromAccessToken(accessToken);

        if (contaId is null)
            return NotFound(new { message = "Cliente não possui conta." });

        if (novoInvestimento.Valor <= 100)
            return BadRequest(new { message = "É necessário investir um valor." });

        novoInvestimento.Tempo = novoInvestimento.Tempo.Date;
        var proximoMes = DateTime.Now.AddMonths(1).Date;

        if (novoInvestimento.Tempo < proximoMes)
            return BadRequest(new { message = "Investimento deve ser de pelo menos um mês." });

        var investimentoResponse = await _investimentoService.CriarInvestimento(contaId.Value, novoInvestimento);
        return CreatedAtAction(nameof(PegarPorId), investimentoResponse);


    }
}
