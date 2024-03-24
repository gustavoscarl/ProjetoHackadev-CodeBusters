using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayWiseBackend.Domain.DTOs;
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponseDTO))]
    public async Task<ActionResult<RetrieveClienteDTO>> PegarPorId()
    {
        string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

        int? id = _authService.GetClienteIdFromToken(accessToken);
        if (!id.HasValue)
            return Unauthorized(new { message = "Cliente não autorizada(o)." });

        var clienteResponse = await _clienteService.BuscarClienteDTOPorId(id.Value);

        if (clienteResponse is null)
            return Unauthorized(new { message = "Cliente não autorizada(o)." });

        return Ok(clienteResponse);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponseDTO))]
    public async Task<ActionResult<CreateClienteResponseDTO>> Cadastrar([FromBody] CreateClientDTO novoCliente)
    {

        bool clientAlreadyExists = await _clienteService.CheckClienteCredentials(novoCliente.Cpf, novoCliente.Rg);

        if (clientAlreadyExists)
            return Conflict(new { message = "Credenciais já cadastradas." });

        var clienteCadastrado = await _clienteService.CadastrarCliente(novoCliente);
        if (clienteCadastrado.Sucesso)
        {
            var clienteResponse = _mapper.Map<CreateClienteResponseDTO>(clienteCadastrado.Data);

            return CreatedAtAction(nameof(PegarPorId), clienteResponse);
        }
        else
            return BadRequest(new { message = clienteCadastrado.MensagemDeErro });

    }
}
