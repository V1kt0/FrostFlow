using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FrostFlow.Models
{
  public class User: IdentityUser
  {
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
  }
}
