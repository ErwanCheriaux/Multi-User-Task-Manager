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
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        var users = await _dataContext.Users.ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<User>>> GetUser(int id)
    {
        var user = await _dataContext.Users.FindAsync(id);
        if (user == null)
            return NotFound("User not found.");

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        _dataContext.Users.Add(user);
        await _dataContext.SaveChangesAsync();
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<User>> UpdateUser(int id, User updatedUser)
    {
        var dbUser = await _dataContext.Users.FindAsync(id);
        if (dbUser == null)
            return NotFound("User not found.");

        dbUser.FirstName = updatedUser.FirstName;
        dbUser.LastName = updatedUser.LastName;
        dbUser.Email = updatedUser.Email;

        await _dataContext.SaveChangesAsync();
        return Ok(dbUser);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var user = await _dataContext.Users.FindAsync(id);
        if (user == null)
            return NotFound("User not found.");

        _dataContext.Users.Remove(user);
        await _dataContext.SaveChangesAsync();
        return NoContent();
    }
}
