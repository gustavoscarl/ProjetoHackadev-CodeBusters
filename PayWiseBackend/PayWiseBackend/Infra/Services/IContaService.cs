using AutoMapper;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Infra.Services;

public interface IContaService
{
    Task<RetrieveContaDTO> CadastrarConta(int clienteId, CreateContaDTO novaConta);
    Task<Conta?> BuscarContaPorId(int? contaId);
    Task<RetrieveContaDTO> BuscarContaDTOPorId(int? contaId);
    Task<RetrieveContaLimitesDTO> AlterarLimitesConta(Conta conta, UpdateContaLimitesDTO novoLimite);
    Task<Conta?> BuscarContaPorNumero(string numeroConta);
    Task<RetrieveHistoricoDTO> BuscarHistoricoDaConta(int? contaId, DateTime? from, DateTime? to);
    Task Sacar(Conta conta, CreateTransacaoSaqueDTO dadosTransacao);
    Task Depositar(Conta conta, CreateTransacaoDepositoDTO dadosTransacao);
    Task Transferencia(Conta conta, Conta contaDestino, CreateTransacaoTransferenciaDTO dadosTransacao);
    Task CadastrarTransacao(Conta conta, Transacao transacao);
    Task DeleteConta(Cliente cliente, Conta conta);
}
