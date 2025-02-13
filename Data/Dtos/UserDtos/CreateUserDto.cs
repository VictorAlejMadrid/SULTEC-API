﻿using SULTEC_API.Models;
using System.ComponentModel.DataAnnotations;

namespace SULTEC_API.Data.Dtos.UserDtos;

public class CreateUserDto
{
    [Required(ErrorMessage = "The username field is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "The password field is required to signin")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
