using Jared.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Jared.Infrastructure.Persistence;

public class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Epic>? Epics { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<History>? History { get; set; }
    public DbSet<Domain.Models.Task>? Tasks { get; set; }
    public DbSet<Project>? Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
