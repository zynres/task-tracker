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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        EntityTypeBuilder<Employee> employeeBuilder = builder.Entity<Employee>();
        employeeBuilder
            .HasMany(em => em.Requests)
            .WithOne(re => re.Author)
            .HasForeignKey(re => re.AuthorId);
        employeeBuilder
            .HasOne(em => em.AssignetRequest)
            .WithOne(re => re.Assignee)
            .HasForeignKey<Request>(re => re.AssigneeId);

        EntityTypeBuilder<Request> requestBuilder = builder.Entity<Request>();
        
        EntityTypeBuilder<Department> depatmentBuilder = builder.Entity<Department>();
        depatmentBuilder.HasData(DepartmentSeed.Data);
        
        EntityTypeBuilder<Position> positionBuilder = builder.Entity<Position>();
        positionBuilder.HasData(PositionSeed.Data);
    }
}