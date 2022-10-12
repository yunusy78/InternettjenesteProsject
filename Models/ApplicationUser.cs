using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace InternettjenesteProsject.Models;

public class ApplicationUser:IdentityUser
{
    [Required]
    public string Nickname { get; set; } = string.Empty;

}