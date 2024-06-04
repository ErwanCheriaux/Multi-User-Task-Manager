using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiUserTaskManager.Api.Entities;

namespace MultiUserTaskManager.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public AuthController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        // Create a new user with the provided details
        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // Add claims for the user's first name and last name
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, user.FirstName));
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Surname, user.LastName));

            return Ok(new { message = "User registered successfully" });
        }

        // If the registration failed, return the errors
        return BadRequest(result.Errors);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout(SignInManager<User> signInManager)
    {
        await signInManager.SignOutAsync();
        return Ok();
    }

    [Authorize]
    [HttpGet("status")]
    public ActionResult Status() {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var firstname = User.FindFirstValue(ClaimTypes.GivenName);
        var lastname = User.FindFirstValue(ClaimTypes.Surname);
        return Ok(new {email, firstname, lastname});
    }
}

public record RegisterModel(
    [Required] [EmailAddress] string Email,
    [Required] [DataType(DataType.Password)] string Password,
    [Required] string FirstName,
    [Required] string LastName
);
