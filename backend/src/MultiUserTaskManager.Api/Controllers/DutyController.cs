using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiUserTaskManager.Api.Data;
using MultiUserTaskManager.Api.Entities;

namespace MultiUserTaskManager.Api.Controller;

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
    public async Task<ActionResult<List<Duty>>> GetAllDuties()
    {
        var duties = await _dataContext.Duties.ToListAsync();
        return Ok(duties);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<Duty>>> GetDuty(int id)
    {
        var duty = await _dataContext.Duties.FindAsync(id);
        if (duty == null)
            return NotFound("Duty not found.");

        return Ok(duty);
    }

    [HttpPost]
    public async Task<ActionResult<Duty>> CreateDuty(Duty duty)
    {
        _dataContext.Duties.Add(duty);
        await _dataContext.SaveChangesAsync();
        return Ok(duty);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Duty>> UpdateDuty(int id, Duty updatedDuty)
    {
        var dbDuty = await _dataContext.Duties.FindAsync(id);
        if (dbDuty == null)
            return NotFound("Duty not found.");

        dbDuty.User = updatedDuty.User;
        dbDuty.Label = updatedDuty.Label;
        dbDuty.Category = updatedDuty.Category;
        dbDuty.EndDate = updatedDuty.EndDate;
        dbDuty.IsCompleted = updatedDuty.IsCompleted;

        await _dataContext.SaveChangesAsync();
        return Ok(dbDuty);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteDuty(int id)
    {
        var duty = await _dataContext.Duties.FindAsync(id);
        if (duty == null)
            return NotFound("Duty not found.");

        _dataContext.Duties.Remove(duty);
        await _dataContext.SaveChangesAsync();
        return NoContent();
    }
}
