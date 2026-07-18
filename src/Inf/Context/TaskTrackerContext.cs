using Microsoft.EntityFrameworkCore;
using App.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Inf.Context.Seed;

namespace Inf.Context;

public class TaskTrackerContext : DbContext, IDbContext
{
    public TaskTrackerContext(DbContextOptions<TaskTrackerContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Position> Positions { get; set; }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}