namespace App.Features.Employee.Dtos;

public record class CompletedByEmployeeDto(
    int Id, 
    string? Name, 
    int CompletedRequestsCount);