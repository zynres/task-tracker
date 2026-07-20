using App.Features.Department.Dtos;
using App.Features.Position.Dtos;
using App.Features.Request.Dtos;

namespace App.Features.Employee.Dtos;

public record class EmployeeDto(
    int Id, 
    string? Name, 
    string? LastName, 
    string? Patronymic, 
    DepartmentDto? Department, 
    PositionDto? Position, 
    RequestDto[] AssignetRequests);