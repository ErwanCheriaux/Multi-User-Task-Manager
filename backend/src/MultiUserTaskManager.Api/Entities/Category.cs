namespace MultiUserTaskManager.Api.Entities;

public class Category
{
    public int Id { get; set; }
    public User? User { get; set; }
    public required string Name { get; set; }
}
