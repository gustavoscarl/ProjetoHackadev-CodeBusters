using PayWiseBackend.Domain.DTOs;

namespace PayWiseBackend.Infra.Services;

public interface IClienteService
{
    Task<bool> CheckClienteCredentials(string cpf, string rg);
    Task<RetrieveClienteDTO> BuscarClientePorId(int? clienteId);
    Task<RetrieveClienteDTO> CadastrarCliente(CreateClientDTO novoCliente);
}
