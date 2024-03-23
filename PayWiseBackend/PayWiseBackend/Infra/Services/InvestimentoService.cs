using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Infra.Services;

public class InvestimentoService : IInvestimentoService
{
    private readonly PaywiseDbContextSqlite _contextSqlite;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public InvestimentoService(
        PaywiseDbContextSqlite contextSqlite,
        IAuthService authService,
        IMapper mapper
        )
    {
        _contextSqlite = contextSqlite;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<RetrieveInvestimentoDTO> BuscarInvestimento(int? contaId)
    {
        var investimento = await _contextSqlite
            .Investimentos
            .AsNoTracking()
            .SingleOrDefaultAsync(investimento => investimento.ContaId == contaId); 

        var investimentoResponse = _mapper.Map<RetrieveInvestimentoDTO>(investimento);

        return investimentoResponse;
    }

    public async Task<RetrieveInvestimentoDTO> CriarInvestimento(Conta conta, CreateInvestimentoDTO novoInvestimento)
    {
        conta.Saldo -= novoInvestimento.Valor;

        var investimento = _mapper.Map<Investimento>(novoInvestimento);
        investimento.ContaId = conta.Id;

        var investimentoResult = await _contextSqlite.Investimentos.AddAsync(investimento);


        await _contextSqlite.SaveChangesAsync();

        var investimentoResponse = _mapper.Map<RetrieveInvestimentoDTO>(investimentoResult.Entity);

        return investimentoResponse;
    }
}
