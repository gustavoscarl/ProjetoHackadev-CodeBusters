using AutoMapper;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;


namespace PayWiseBackend.Domain.Profiles;

public class ClientResponseProfile : Profile
{
    public ClientResponseProfile()
    {
        CreateMap<Cliente, RetrieveClienteDTO>();
    }
}
