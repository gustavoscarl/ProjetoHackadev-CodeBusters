using AutoMapper;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Domain.Profiles
{
    public class ContaProfile : Profile
    {
        public ContaProfile() : base()
        {
            CreateMap<CreateContaDTO, Conta>();        
        }
    }
}
