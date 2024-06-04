using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiUserTaskManager.Api.Data;
using MultiUserTaskManager.Api.Entities;

namespace MultiUserTaskManager.Api.Controller;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class DutyController : ControllerBase
{
    private readonly DataContext _dataContext;

    public DutyController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<DutyDto>>> GetAllDuties()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var duties = await _dataContext
            .Duties.Where(duty => duty.User != null && duty.User.Email == email)
            .Select(duty => duty.AsDto())
            .ToListAsync();

        return Ok(duties);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<DutyDto>>> GetDuty(int id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var duty = await _dataContext.Duties.FirstOrDefaultAsync(d =>
            d.Id == id && d.User != null && d.User.Email == email
        );

        if (duty == null)
            return NotFound("Duty not found.");

        return Ok(duty.AsDto());
    }

    [HttpPost]
    public async Task<ActionResult<DutyDto>> CreateDuty(DutyModel model)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        // Find the existing user based on user email
        var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            return NotFound("User not found");

        // Create a new Duty entity with the existing user
        var duty = new Duty
        {
            Label = model.Label,
            Category = model.Category,
            EndDate = model.EndDate,
            IsCompleted = model.IsCompleted,
            User = user
        };

        _dataContext.Duties.Add(duty);
        await _dataContext.SaveChangesAsync();
        return Ok(duty.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DutyDto>> UpdateDuty(int id, Duty updatedDuty)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var duty = await _dataContext.Duties.FirstOrDefaultAsync(d =>
            d.Id == id && d.User != null && d.User.Email == email
        );

        if (duty == null)
            return NotFound("Duty not found.");

        duty.User = updatedDuty.User;
        duty.Label = updatedDuty.Label;
        duty.Category = updatedDuty.Category;
        duty.EndDate = updatedDuty.EndDate;
        duty.IsCompleted = updatedDuty.IsCompleted;

        await _dataContext.SaveChangesAsync();
        return Ok(duty.AsDto());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDuty(int id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var duty = await _dataContext.Duties.FirstOrDefaultAsync(d =>
            d.Id == id && d.User != null && d.User.Email == email
        );

        if (duty == null)
            return NotFound("Duty not found.");

        _dataContext.Duties.Remove(duty);
        await _dataContext.SaveChangesAsync();
        return NoContent();
    }
}

public record DutyModel(
    [Required] string Label,
    [Required] string Category,
    [Required] DateTime EndDate,
    [Required] bool IsCompleted
);
