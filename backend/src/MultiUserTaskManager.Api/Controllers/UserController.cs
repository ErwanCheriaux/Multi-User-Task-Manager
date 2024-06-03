using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiUserTaskManager.Api.Data;
using MultiUserTaskManager.Api.Entities;

namespace MultiUserTaskManager.Api.Controller;

[ApiController]
[Route("api/[Controller]")]
public class UserController : ControllerBase
{
    private readonly DataContext _dataContext;

    public UserController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        var users = await _dataContext.Users.ToListAsync();
        return Ok(users.Select(user => user.AsDto()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<UserDto>>> GetUser(string id)
    {
        var user = await _dataContext.Users.FindAsync(id);
        if (user == null)
            return NotFound("User not found.");

        return Ok(user.AsDto());
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        _dataContext.Users.Add(user);
        await _dataContext.SaveChangesAsync();
        return Ok(user.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(string id, User updatedUser)
    {
        var dbUser = await _dataContext.Users.FindAsync(id);
        if (dbUser == null)
            return NotFound("User not found.");

        dbUser.FirstName = updatedUser.FirstName;
        dbUser.LastName = updatedUser.LastName;

        await _dataContext.SaveChangesAsync();
        return Ok(dbUser.AsDto());
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUser(string id)
    {
        var user = await _dataContext.Users.FindAsync(id);
        if (user == null)
            return NotFound("User not found.");

        _dataContext.Users.Remove(user);
        await _dataContext.SaveChangesAsync();
        return NoContent();
    }
}
