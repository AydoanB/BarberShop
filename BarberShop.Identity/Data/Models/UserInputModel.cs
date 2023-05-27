﻿using System.ComponentModel.DataAnnotations;

namespace BarberShop.Identity.Data.Models;

public class UserInputModel
{
    [EmailAddress]
    [Required]
    [MinLength(10)]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}