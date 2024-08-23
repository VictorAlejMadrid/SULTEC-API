using System.ComponentModel.DataAnnotations;

namespace SULTEC_API.Data.Dtos.UserDtos;

public class ChangePasswordUserDto
{
    [Required(ErrorMessage = "User Identifier is required")]
    public string Id { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Current password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "New password is required")]
    public string NewPassword { get; set; }
}
