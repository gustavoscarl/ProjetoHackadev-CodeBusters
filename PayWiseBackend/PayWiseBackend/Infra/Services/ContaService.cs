using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
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
}
