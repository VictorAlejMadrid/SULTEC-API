using Microsoft.AspNetCore.Identity;
using SULTEC_API.Data.Dtos.UserDtos;
using System.ComponentModel.DataAnnotations;

namespace SULTEC_API.Models;

public class User  : IdentityUser
{
    public User() : base() { }

    public static implicit operator User(CreateUserDto user)
        => new User()
        {
            UserName = user.UserName,
        };
}
