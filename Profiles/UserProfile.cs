using AutoMapper;
using SULTEC_API.Data.Dtos.UserDtos;
using SULTEC_API.Models;

namespace SULTEC_API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>();

        CreateMap<User, ReadUserDto>();
        CreateMap<ChangePasswordUserDto, User>();

    }
}
