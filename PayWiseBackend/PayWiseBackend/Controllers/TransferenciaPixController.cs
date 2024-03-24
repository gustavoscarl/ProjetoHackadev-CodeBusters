using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Infra.Services;
using System.Threading.Tasks;

namespace PayWiseBackend.Controllers
{
    [Route("contas/[controller]")]
    [ApiController]
    public class TransferenciaPixController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITransferenciaPixService _transferenciaPixService;

        public TransferenciaPixController(IAuthService authService, ITransferenciaPixService transferenciaPixService)
        {
            _authService = authService;
            _transferenciaPixService = transferenciaPixService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseDTO))]
        public async Task<ActionResult<TransferenciaPixResponseDTO>> RealizarTransferenciaPix(CreateTransferenciaPixDTO transferenciaPixDTO)
        {
            string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

            int? remetenteId = _authService.GetContaIdFromAccessToken(accessToken);

            if (!remetenteId.HasValue)
                return Unauthorized(new { message = "Cliente não autorizado(a)." });

            // Aqui você pode adicionar validações adicionais, como verificar se o remetente possui saldo suficiente, etc.

            var resultado = await _transferenciaPixService.RealizarTransferenciaPix(remetenteId.Value, transferenciaPixDTO);

            if (resultado.Sucesso)
                return CreatedAtAction(nameof(ConsultarTransferenciaPix), new { id = resultado.Dados.Id }, resultado.Dados);
            else
                return BadRequest(new { message = resultado.Mensagem });
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDTO))]
        public async Task<ActionResult<TransferenciaPixDTO>> ConsultarTransferenciaPix(int id)
        {
            // Aqui você pode implementar a lógica para buscar e retornar os detalhes de uma transferência PIX por seu ID.
            // Certifique-se de que apenas o remetente ou destinatário da transferência tenha acesso aos detalhes.

            return Ok();
        }

        // Adicione outros métodos conforme necessário, como listar todas as transferências PIX de um cliente, etc.
    }
}
