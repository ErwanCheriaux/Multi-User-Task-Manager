using Microsoft.AspNetCore.Identity;

namespace MultiUserTaskManager.Api.Entities;

public class User : IdentityUser
{
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
}
