using AutoMapper;
using SULTEC_API.Data.Dtos.ClientDtos;
using SULTEC_API.Models;

namespace SULTEC_API.Profiles;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<Client, ReadClientDto>();
        CreateMap<ReadClientDto, Client>();
        CreateMap<CreateClientDto, Client>();
    }
}
