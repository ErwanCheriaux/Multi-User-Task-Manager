using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiUserTaskManager.Api.Entities;

namespace MultiUserTaskManager.Api.Data;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    public DbSet<Duty> Duties { get; set; }
}
