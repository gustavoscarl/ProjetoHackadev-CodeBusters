using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Infra.Services;

public interface IInvestimentoService
{
    Task<RetrieveInvestimentoDTO> CriarInvestimento(Conta conta, CreateInvestimentoDTO novoInvestimento);
    Task<RetrieveInvestimentoDTO> BuscarInvestimento(int? contaId);
}
