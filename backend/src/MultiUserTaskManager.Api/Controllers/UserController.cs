using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiUserTaskManager.Api.Data;

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

    [HttpPut]
    public async Task<ActionResult<UserDto>> UpdateUser(UserModel updatedModel)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _dataContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        if (user == null)
            return NotFound("User not found.");

        user.FirstName = updatedModel.FirstName;
        user.LastName = updatedModel.LastName;

        await _dataContext.SaveChangesAsync();
        return Ok(user.AsDto());
    }
}

public record UserModel([Required] string FirstName, [Required] string LastName);
