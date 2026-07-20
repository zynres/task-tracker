using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace App.Interfaces;

public interface IDbContext
{
    DbSet<Employee> Employees { get; set; }
    DbSet<Request> Requests { get; set; }
    DbSet<Department> Departments { get; set; }
    DbSet<Position> Positions { get; set; }

    Task<int> SaveChangesAsync();
    ChangeTracker GetChangeTracker();
}