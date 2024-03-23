using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Enum;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Infra.Services;

public class ContaService : IContaService
{
    private readonly PaywiseDbContextSqlite _contextSqlite;
    private readonly IMapper _mapper;
    private readonly IClienteService _clienteService;

    public ContaService(
        PaywiseDbContextSqlite contextSqlite,
        IMapper mapper,
        IClienteService clienteService
        )
    {
        _contextSqlite = contextSqlite;
        _mapper = mapper;
        _clienteService = clienteService;
    }
    public async Task<RetrieveContaDTO> CadastrarConta(int clienteId, CreateContaDTO novaConta)
    {
        int numConta = int.Parse(await _contextSqlite.Contas.MaxAsync(conta => conta.Numero) ?? "0000");
        numConta += 1;
        Conta contaCadastrar = new Conta()
        {
            Numero = numConta.ToString("D6"),
            Pin = novaConta.Pin,
            ClienteId = clienteId
        };
        var result = await _contextSqlite.Contas.AddAsync(contaCadastrar);
        await _contextSqlite.SaveChangesAsync();

        var contaCadastrada = result.Entity;

        Historico historico = new Historico()
        {
            ContaId = contaCadastrada.Id
        };

        await _contextSqlite.Historicos.AddAsync(historico);

        var cliente = await _contextSqlite.Clientes.FindAsync(clienteId);
        cliente!.TemConta = true;

        await _contextSqlite.SaveChangesAsync();

        var contaResponse = _mapper.Map<RetrieveContaDTO>(contaCadastrada);

        return contaResponse;
    }

    public async Task<Conta?> BuscarContaPorId(int? contaId)
    {
        var conta = await _contextSqlite.Contas.FindAsync(contaId);

        if (conta is null)
            return null;

        return conta;

    }

    public async Task<Conta?> BuscarContaPorNumero(string numeroConta)
    {
        var conta = await _contextSqlite.Contas.FirstOrDefaultAsync(conta => conta.Numero == numeroConta);

        if (conta is null)
            return null;

        return conta;

    }

    public async Task<RetrieveHistoricoDTO> BuscarHistoricoDaConta(int? contaId, DateTime? from, DateTime? to)
    {
        var historico = await _contextSqlite.Historicos.FirstOrDefaultAsync(h => h.ContaId == contaId);

        DateTime agora = DateTime.Now, de, ate;
        de = new DateTime(agora.Year, agora.Month, 1);
        ate = de.AddMonths(1);
        IEnumerable<Transacao>? transacoes;
        
        if(from.HasValue && to.HasValue)
        {
            transacoes = historico
            .Transacoes
            .Where(t => t.Horario >= from && t.Horario <= to);
        }
        else if (from.HasValue && !to.HasValue)
        {
            transacoes = historico.Transacoes.Where(t => t.Horario == from.Value);
        }
        else
        {
            transacoes = historico
            .Transacoes
            .Where(t => t.Horario >= de && t.Horario < ate);
        }

        var historicoResponse = _mapper.Map<RetrieveHistoricoDTO>(transacoes);
        return historicoResponse;
    }

    public async Task Sacar(Conta conta, CreateTransacaoSaqueDTO dadosTransacao)
    {
        conta.Saldo -= dadosTransacao.Valor;

        Transacao transacao = new Transacao()
        {
            Descricao = dadosTransacao.Descricao ?? string.Empty,
            Horario = DateTime.Now,
            Tipo = TransacaoTipo.SAQUE,
            Valor = dadosTransacao.Valor,
        };
        await _contextSqlite.SaveChangesAsync();
        await CadastrarTransacao(conta, transacao);
    }

    public async Task Depositar(Conta conta, CreateTransacaoDepositoDTO dadosTransacao)
    {
        conta.Saldo += dadosTransacao.Valor;

        Transacao transacao = new Transacao()
        {
            Descricao = dadosTransacao.Descricao ?? string.Empty,
            Horario = DateTime.Now,
            Tipo = TransacaoTipo.DEPOSITO,
            Valor = dadosTransacao.Valor,
        };
        await _contextSqlite.SaveChangesAsync();
        await CadastrarTransacao(conta, transacao);

    }

    public async Task Transferencia(Conta conta, Conta contaDestino, CreateTransacaoTransferenciaDTO dadosTransacao)
    {
        conta.Saldo -= dadosTransacao.Valor;
        contaDestino.Saldo += dadosTransacao.Valor;
        await _contextSqlite.SaveChangesAsync();

        Transacao transacao = new Transacao()
        {
            Descricao = dadosTransacao.Descricao ?? string.Empty,
            Horario = DateTime.Now,
            Tipo = TransacaoTipo.TRANSFERENCIA,
            Valor = dadosTransacao.Valor,
        };

        await CadastrarTransacao(conta, transacao);

        Transacao transacaoDestino = new Transacao()
        {
            Descricao = dadosTransacao.Descricao ?? string.Empty,
            Horario = DateTime.Now,
            Tipo = TransacaoTipo.TRANSFERENCIA,
            Valor = dadosTransacao.Valor,
        };

        await CadastrarTransacao(contaDestino, transacaoDestino);
    }

    public async Task CadastrarTransacao(Conta conta, Transacao transacao)
    {
        conta.Historico.Transacoes.Add(transacao);
        await _contextSqlite.SaveChangesAsync();
    }

    public async Task DeleteConta(Cliente cliente, Conta conta)
    {
        cliente.TemConta = false;

        conta.EstaAtiva = false;

        await _contextSqlite.SaveChangesAsync();
    }

    public async Task<RetrieveContaLimitesDTO> AlterarLimitesConta(Conta conta, UpdateContaLimitesDTO novoLimite)
    {
        conta.LimitePixGeral = novoLimite.LimitePixGeral;
        conta.LimitePixNoturno = novoLimite.LimitePixNoturno;
        conta.DataModificacao = DateTime.Now;
        _contextSqlite.Contas.Update(conta);
        await _contextSqlite.SaveChangesAsync();

        var contaResponse = _mapper.Map<RetrieveContaLimitesDTO>(conta);
        return contaResponse;
    }

    public async Task<RetrieveContaDTO> BuscarContaDTOPorId(int? contaId)
    {
        var conta = await BuscarContaDTOPorId(contaId);
        var contaResponse = _mapper.Map<RetrieveContaDTO>(conta);
        return contaResponse;
    }
}