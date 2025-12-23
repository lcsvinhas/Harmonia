using System.ComponentModel.DataAnnotations;

namespace Harmonia.API.DTOs;

public class LoginRequestDTO
{
    [Required(ErrorMessage = "Username is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}
