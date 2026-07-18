using Domain.Entities;

namespace Inf.Context.Seed;

public static class PositionSeed
{
    public static readonly Position[] Data = [
        new Position() { Id = 1, Name = "Chief Executive Officer (CEO)"},
        new Position() { Id = 2, Name = "HR Manager"},
        new Position() { Id = 3, Name = "Software Developer"},
        new Position() { Id = 4, Name = "System Administrator"},
        new Position() { Id = 5, Name = "Accountant"},
        new Position() { Id = 6, Name = "Sales Manager"},
        new Position() { Id = 7, Name = "Marketing Specialist"},
        new Position() { Id = 8, Name = "Customer Support Specialist"},
        new Position() { Id = 9, Name = "Legal Consel"},
        new Position() { Id = 10, Name = "Logistics Coordinator"}
    ];
}