using PayWiseBackend.Domain.DTOs;
using System.Threading.Tasks;

namespace PayWiseBackend.Infra.Services
{
    public interface ITransferenciaPixService
    {
        Task<Result<TransferenciaPixResponseDTO>> RealizarTransferenciaPix(int remetenteId, CreateTransferenciaPixDTO transferenciaPixDTO);
    }
}
