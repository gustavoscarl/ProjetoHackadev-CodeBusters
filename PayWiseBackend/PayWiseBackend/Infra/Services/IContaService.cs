using AutoMapper;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Infra.Services;

public interface IContaService
{
    Task<RetrieveContaDTO> CadastrarConta(int clienteId, CreateContaDTO novaConta);
    Task<Conta?> BuscarContaPorId(int? contaId);
    Task<Historico> BuscarHistoricoDaConta(int? contaId);
}
