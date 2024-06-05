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
public class CategoryController : ControllerBase
{
    private readonly DataContext _dataContext;

    public CategoryController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAllCategories()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var categories = await _dataContext
            .Categories.Where(category => category.User == null || category.User.Email == email)
            .Select(category => category.AsDto())
            .ToListAsync();

        return Ok(categories);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryModel model)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        // Find the existing user based on user email
        var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            return NotFound("User not found");

        // Create a new Category entity with the existing user
        var category = new Category
        {
            Name = model.Name,
            User = user
        };

        _dataContext.Categories.Add(category);
        await _dataContext.SaveChangesAsync();
        return Ok(category.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, Category updatedCategory)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var category = await _dataContext.Categories.FirstOrDefaultAsync(d =>
            d.Id == id && d.User != null && d.User.Email == email
        );

        if (category == null)
            return NotFound("Category not found.");

        category.Name = updatedCategory.Name;

        await _dataContext.SaveChangesAsync();
        return Ok(category.AsDto());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var category = await _dataContext.Categories.FirstOrDefaultAsync(d =>
            d.Id == id && d.User != null && d.User.Email == email
        );

        if (category == null)
            return NotFound("Category not found.");

        _dataContext.Categories.Remove(category);
        await _dataContext.SaveChangesAsync();
        return NoContent();
    }
}

public record CategoryModel(
    [Required] string Name
);
