using MultiUserTaskManager.Api.Entities;

namespace MultiUserTaskManager.Api;

public static class Extensions
{
    public static UserDto AsDto(this User user)
    {
        return new(user.Id, user.Email, user.LastName, user.FirstName);
    }

    public static DutyDto AsDto(this Duty duty)
    {
        return new(duty.Id, duty.Label, duty.Category?.Id, duty.EndDate, duty.IsCompleted);
    }

    public static CategoryDto AsDto(this Category category)
    {
        bool isReadOnly = category.User == null;
        return new(category.Id, category.Name, isReadOnly);
    }
}
