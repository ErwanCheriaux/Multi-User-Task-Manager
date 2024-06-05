using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiUserTaskManager.Api.Data;
using MultiUserTaskManager.Api.Entities;

namespace MultiUserTaskManager.Api.Controller;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class UserController : ControllerBase
{
    private readonly DataContext _dataContext;

    public UserController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(string id, User updatedUser)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _dataContext.Users.FindAsync(id);
        if (user == null || user.Email != email)
            return NotFound("User not found.");

        user.FirstName = updatedUser.FirstName;
        user.LastName = updatedUser.LastName;

        await _dataContext.SaveChangesAsync();
        return Ok(user.AsDto());
    }
}
