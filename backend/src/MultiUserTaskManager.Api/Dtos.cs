using System.ComponentModel.DataAnnotations;

namespace MultiUserTaskManager.Api;

public record UserDto(
    [Required] string Id,
    [Required] string? Email,
    [Required] string? LastName,
    [Required] string? FirstName
);

public record DutyDto(
    [Required] int Id,
    [Required] string Label,
    [Required] int? CategoryId,
    [Required] DateTime EndDate,
    [Required] bool IsCompleted
);

public record CategoryDto([Required] int Id, [Required] string Name, [Required] bool IsReadOnly);
