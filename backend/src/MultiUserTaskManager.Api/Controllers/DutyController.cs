using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
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
            .Duties.Include(duty => duty.Category)
            .Where(duty => duty.User != null && duty.User.Email == email)
            .Select(duty => duty.AsDto())
            .ToListAsync();

        return Ok(duties);
    }

    [HttpPost]
    public async Task<ActionResult<DutyDto>> CreateDuty(DutyModel model)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        // Find the existing user based on user email
        var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            return NotFound("User not found");

        // Find the existing category based on category id or assign no category
        var category = await _dataContext.Categories.FirstOrDefaultAsync(d =>
            d.Id == model.CategoryId
            && (d.User == null || (d.User != null && d.User.Email == email))
        );

        // Create a new Duty entity with the existing user
        var duty = new Duty
        {
            User = user,
            Label = model.Label,
            Category = category,
            EndDate = model.EndDate,
            IsCompleted = model.IsCompleted
        };

        _dataContext.Duties.Add(duty);
        await _dataContext.SaveChangesAsync();
        return Ok(duty.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DutyDto>> UpdateDuty(int id, DutyModel updatedModel)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var duty = await _dataContext
            .Duties.Include(duty => duty.Category)
            .FirstOrDefaultAsync(d => d.Id == id && d.User != null && d.User.Email == email);

        if (duty == null)
            return NotFound("Duty not found.");

        // Find the existing category based on category id or assign no category
        var category = await _dataContext.Categories.FirstOrDefaultAsync(d =>
            d.Id == updatedModel.CategoryId
            && (d.User == null || (d.User != null && d.User.Email == email))
        );

        duty.Label = updatedModel.Label;
        duty.Category = category;
        duty.EndDate = updatedModel.EndDate;
        duty.IsCompleted = updatedModel.IsCompleted;

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
    [Optional] int? CategoryId,
    [Required] DateTime EndDate,
    [Required] bool IsCompleted
);
