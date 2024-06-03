using Microsoft.AspNetCore.Identity;

namespace MultiUserTaskManager.Api.Entities;

public class User : IdentityUser
{
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
}
