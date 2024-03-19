using AutoMapper;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Domain.Profiles;

public class TransacaoProfile : Profile
{
    public TransacaoProfile()
    {
        CreateMap<Transacao, RetrieveTransacaoDTO>();
    }
}
