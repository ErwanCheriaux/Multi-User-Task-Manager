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
        return new(duty.Id, duty.Label, duty.Category, duty.EndDate, duty.IsCompleted);
    }
}
