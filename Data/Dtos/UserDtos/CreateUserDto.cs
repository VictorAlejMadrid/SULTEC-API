using SULTEC_API.Models;
using System.ComponentModel.DataAnnotations;

namespace SULTEC_API.Data.Dtos.UserDtos;

public class CreateUserDto
{
    [Required(ErrorMessage = "O nome do usuário é obrigatório")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
