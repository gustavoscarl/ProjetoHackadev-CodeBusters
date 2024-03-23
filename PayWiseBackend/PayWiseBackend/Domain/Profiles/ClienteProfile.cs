using AutoMapper;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Domain.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile() : base()
        {
            CreateMap<CreateClientDTO, Cliente>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco));
            CreateMap<Cliente, RetrieveClienteDTO>()
                .ForMember(dest => dest.ContaId, opt => {
                    opt.PreCondition(src => src.Conta != null && src.Conta.EstaAtiva);
                    opt.MapFrom(src => src.Conta.Id);
                });
            CreateMap<Cliente, CreateClienteResponseDTO>()
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src));
        }
    }
}
