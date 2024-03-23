using AutoMapper;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Domain.Profiles;

public class InvestimentoProfile : Profile
{
    public InvestimentoProfile()
    {
        CreateMap<Investimento, RetrieveInvestimentoDTO>();
        CreateMap<CreateInvestimentoDTO, Investimento>();
    }
}
