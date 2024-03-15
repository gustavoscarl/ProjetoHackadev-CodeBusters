using AutoMapper;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Domain.Profiles;

public class ContaResponseProfile : Profile
{
    public ContaResponseProfile()
    {
        CreateMap<Conta, RetrieveContaDTO>();
    }
}
