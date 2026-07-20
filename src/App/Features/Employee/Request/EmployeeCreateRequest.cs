namespace App.Features.Employee.Request;

public record class EmployeeCreateRequest(
    string? Name, 
    string? LastName, 
    string? Patronymic,
    int DepartmentId, 
    int PositionId, 
    int Count);