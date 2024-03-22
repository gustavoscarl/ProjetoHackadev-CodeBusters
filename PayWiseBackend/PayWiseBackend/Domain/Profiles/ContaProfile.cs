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
            CreateMap<Conta, RetrieveContaDTO>();
            CreateMap<Conta, RetrieveContaLimitesDTO>();

            CreateMap<(RetrieveContaDTO conta, string accessToken), CreateContaResponseDTO>()
                .ForMember(dest => dest.Conta, opt => opt.MapFrom(src => src.conta))
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.accessToken));

            CreateMap<Conta, RetrieveSaldoDTO>();

        }
    }
}
