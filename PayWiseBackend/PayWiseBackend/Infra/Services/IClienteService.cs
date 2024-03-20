using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Infra.Services;

public interface IClienteService
{
    Task<bool> CheckClienteCredentials(string cpf, string rg);
    Task<Cliente> BuscarClientePorId(int? clienteId);
    Task<RetrieveClienteDTO> CadastrarCliente(CreateClientDTO novoCliente);
}
