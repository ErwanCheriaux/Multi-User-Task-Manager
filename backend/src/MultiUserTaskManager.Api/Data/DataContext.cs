using Microsoft.EntityFrameworkCore;
using MultiUserTaskManager.Api.Entities;

namespace MultiUserTaskManager.Api.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Duty> Duties { get; set; }
}
