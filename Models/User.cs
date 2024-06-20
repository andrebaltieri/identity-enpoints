using Microsoft.AspNetCore.Identity;

namespace ApiIdentityEndpoint.Models;

public class User : IdentityUser
{
    public string Document { get; set; } = string.Empty;
}