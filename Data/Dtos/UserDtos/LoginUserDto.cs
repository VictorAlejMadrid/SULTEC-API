using System.ComponentModel.DataAnnotations;

namespace SULTEC_API.Data.Dtos.UserDtos;

public class LoginUserDto
{
    [Required(ErrorMessage = "The username field is required to signin")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "The password field is required to signin")]
    public string Password { get; set; }
}
