using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Enum;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Infra.Services;

public class ContaService : IContaService
{
    private readonly PaywiseDbContext _context;
    private readonly IMapper _mapper;
    private readonly IClienteService _clienteService;

    public ContaService(
        PaywiseDbContext context,
        IMapper mapper,
        IClienteService clienteService
        )
    {
        _context = context;
        _mapper = mapper;
        _clienteService = clienteService;
    }
    public async Task<RetrieveContaDTO> CadastrarConta(int clienteId, CreateContaDTO novaConta)
    {
        int numConta = int.Parse(await _context.Contas.MaxAsync(conta => conta.Numero) ?? "0000");
        numConta += 1;
        Conta contaCadastrar = new Conta()
        {
            Numero = numConta.ToString("D6"),
            Pin = novaConta.Pin,
            ClienteId = clienteId
        };
        var result = await _context.Contas.AddAsync(contaCadastrar);
        await _context.SaveChangesAsync();

        var contaCadastrada = result.Entity;

        Historico historico = new Historico()
        {
            ContaId = contaCadastrada.Id
        };

        await _context.Historicos.AddAsync(historico);

        var cliente = await _context.Clientes.FindAsync(clienteId);
        cliente!.TemConta = true;

        await _context.SaveChangesAsync();

        var contaResponse = _mapper.Map<RetrieveContaDTO>(contaCadastrada);

        return contaResponse;
    }

    public async Task<Conta?> BuscarContaPorId(int? contaId)
    {
        var conta = await _context.Contas.FindAsync(contaId);

        if (conta is null)
            return null;

        return conta;

    }

    public async Task<Historico> BuscarHistoricoDaConta(int? contaId)
    {
        var historico = await _context.Historicos.FirstOrDefaultAsync(h => h.ContaId == contaId);
        return historico;
    }

    public async Task Sacar(Conta conta, CreateTransacaoSaqueDTO dadosTransacao)
    {
        conta.Saldo -= dadosTransacao.Valor;

        Transacao transacao = new Transacao()
        {
            Descricao = dadosTransacao.Descricao ?? string.Empty,
            Horario = new DateTime(),
            Tipo = TransacaoTipo.SAQUE,
            Valor = dadosTransacao.Valor,
        };
        await _context.SaveChangesAsync();
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
        await _context.SaveChangesAsync();
        await CadastrarTransacao(conta, transacao);

    }

    public async Task Transferencia(Conta conta, Conta contaDestino, CreateTransacaoTransferenciaDTO dadosTransacao)
    {
        conta.Saldo -= dadosTransacao.Valor;
        contaDestino.Saldo += dadosTransacao.Valor;
        await _context.SaveChangesAsync();

        Transacao transacao = new Transacao()
        {
            Descricao = dadosTransacao.Descricao ?? string.Empty,
            Horario = new DateTime(),
            Tipo = TransacaoTipo.TRANSFERENCIA,
            Valor = dadosTransacao.Valor,
        };

        await CadastrarTransacao(conta, transacao);

        Transacao transacaoDestino = new Transacao()
        {
            Descricao = dadosTransacao.Descricao ?? string.Empty,
            Horario = new DateTime(),
            Tipo = TransacaoTipo.TRANSFERENCIA,
            Valor = dadosTransacao.Valor,
        };

        await CadastrarTransacao(contaDestino, transacaoDestino);
    }

    public async Task CadastrarTransacao(Conta conta, Transacao transacao)
    {
        conta.Historico.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteConta(Cliente cliente, Conta conta)
    {
        cliente.TemConta = false;

        conta.EstaAtiva = false;

        await _context.SaveChangesAsync();
    }
}
