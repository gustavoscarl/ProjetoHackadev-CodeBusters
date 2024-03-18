using AutoMapper;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Domain.Profiles;

public class HistoricoResponseProfile : Profile
{
    public HistoricoResponseProfile()
    {
        CreateMap<Historico, RetrieveHistoricoDTO>()
            .ForMember(dest => dest.Transacoes, opt => opt.MapFrom(src => src.Transacoes));
    }
}
