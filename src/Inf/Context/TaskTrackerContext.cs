using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Inf.Context.Seed;
using Domain.Entities;
using App.Interfaces;

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

    public ChangeTracker GetChangeTracker()
    {
        return ChangeTracker;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        EntityTypeBuilder<Employee> employeeBuilder = builder.Entity<Employee>();
        employeeBuilder
            .HasMany(em => em.Requests)
            .WithOne(re => re.Author)
            .HasForeignKey(re => re.AuthorId);
        employeeBuilder
            .HasMany(em => em.AssignedRequests)
            .WithOne(re => re.Assignee)
            .HasForeignKey(re => re.AssigneeId);

        EntityTypeBuilder<Request> requestBuilder = builder.Entity<Request>();
        requestBuilder
            .HasIndex(r => new
            {
                r.AssigneeId,
                r.Status,
                r.DeadLine
            });

        EntityTypeBuilder<Department> depatmentBuilder = builder.Entity<Department>();
        depatmentBuilder.HasData(DepartmentSeed.Data);

        EntityTypeBuilder<Position> positionBuilder = builder.Entity<Position>();
        positionBuilder.HasData(PositionSeed.Data);
    }
}