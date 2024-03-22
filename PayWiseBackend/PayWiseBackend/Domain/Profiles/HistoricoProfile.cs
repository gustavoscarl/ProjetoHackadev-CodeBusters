using AutoMapper;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Domain.Profiles;

public class HistoricoProfile : Profile
{
    public HistoricoProfile()
    {
        CreateMap<IEnumerable<Transacao>, RetrieveHistoricoDTO>()
            .ForMember(dest => dest.Transacoes, opt => opt.MapFrom(src => src));
    }
}
