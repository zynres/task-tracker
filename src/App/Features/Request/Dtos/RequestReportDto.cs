using App.Features.Employee.Dtos;

namespace App.Features.Request.Dtos;

public record class RequestReportDto(
    int NewStatusesCount, 
    int InProgresStatusesCount, 
    int CompletedStatusesCount, 
    int OverduesCount, 
    CompletedByEmployeeDto[] EmployeeDto);