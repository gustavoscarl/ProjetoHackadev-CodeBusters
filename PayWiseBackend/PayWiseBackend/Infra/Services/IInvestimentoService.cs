using PayWiseBackend.Domain.DTOs;

namespace PayWiseBackend.Infra.Services;

public interface IInvestimentoService
{
    Task<RetrieveInvestimentoDTO> CriarInvestimento(int contaId, CreateInvestimentoDTO novoInvestimento);
    Task<RetrieveInvestimentoDTO> BuscarInvestimento(int? contaId);
}
