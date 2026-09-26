using System.ComponentModel.DataAnnotations;

namespace CrottoPlinius.Models;

public class AdminUser
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime? LastLoginAt { get; set; }
}
