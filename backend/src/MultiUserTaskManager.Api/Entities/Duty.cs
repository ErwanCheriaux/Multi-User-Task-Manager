namespace MultiUserTaskManager.Api.Entities;

public class Duty
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public required string Category { get; set; }
    public required DateTime EndDate { get; set; }
    public bool IsCompleted { get; set; } = false;
}
