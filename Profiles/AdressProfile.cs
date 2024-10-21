using AutoMapper;
using SULTEC_API.Data.Dtos.AdressDtos;
using SULTEC_API.Models;

namespace SULTEC_API.Profiles;

public class AdressProfile : Profile
{
    public AdressProfile()
    {
        CreateMap<Address, ReadAddressDto>();
        CreateMap<CreateAddressDto, Address>();
        CreateMap<ReadAddressDto, Address>();
    }
}
