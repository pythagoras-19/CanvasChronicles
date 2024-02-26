using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PersonalBlog2.Models;

public class User: IdentityUser
{
    /*
    [Key] // Denotes this property as the primary key
    public int UserID { get; set; }

    [Required] // Marks this field as required
    [MaxLength(255)] // Sets a maximum length for the username
    public string Username { get; set; }

    [Required]
    [EmailAddress] // Validates the property as an email address
    [MaxLength(255)]
    public string Email{ get; set; }

    [Required]
    public string PasswordHash { get; set; }
    */

    public DateTime CreatedAt { get; set; } = DateTime.Now; // Default to current time

    public DateTime LastModified { get; set; } = DateTime.Now; // Default to current time
}