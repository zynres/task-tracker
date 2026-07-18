using Domain.Entities;

namespace Inf.Context.Seed;

public static class DepartmentSeed
{
    public static readonly Department[] Data = [
        new Department() { Id = 1, Name = "Administration"},
        new Department() { Id = 2, Name = "Human Resources"},
        new Department() { Id = 3, Name = "Information Technalogy"},
        new Department() { Id = 4, Name = "Finance"},
        new Department() { Id = 5, Name = "Accounting"},
        new Department() { Id = 6, Name = "Sales"},
        new Department() { Id = 7, Name = "Marketing"},
        new Department() { Id = 8, Name = "Customer Support"},
        new Department() { Id = 9, Name = "Logistics"},
        new Department() { Id = 10, Name = "Legal"}
    ];
}