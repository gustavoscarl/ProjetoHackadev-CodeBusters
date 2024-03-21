using AutoMapper;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Infra.Services;

public interface IContaService
{
    Task<RetrieveContaDTO> CadastrarConta(int clienteId, CreateContaDTO novaConta);
    Task<Conta?> BuscarContaPorId(int? contaId);
    Task<Conta?> BuscarContaPorNumero(string numeroConta);
    Task<Historico> BuscarHistoricoDaConta(int? contaId);
    Task Sacar(Conta conta, CreateTransacaoSaqueDTO dadosTransacao);
    Task Depositar(Conta conta, CreateTransacaoDepositoDTO dadosTransacao);
    Task Transferencia(Conta conta, Conta contaDestino, CreateTransacaoTransferenciaDTO dadosTransacao);
    Task CadastrarTransacao(Conta conta, Transacao transacao);
    Task DeleteConta(Cliente cliente, Conta conta);
}
