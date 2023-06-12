using System.ComponentModel.DataAnnotations;

namespace BarberShop.Identity.Models;

public class UserInputModel
{
    [EmailAddress]
    [Required]
    [MinLength(10)]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [MinLength(10)]
    [MaxLength(100)]
    public string FullName { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public bool IsBarber { get; set; }
}