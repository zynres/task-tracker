namespace App.Features.Request.Dtos;

public record class RequestDto(
    int Id, 
    int AuthorId, 
    int? AssigneeId, 
    string? Description, 
    DateTime CreatedDate, 
    DateTime DeadLine,
    DateTime? CompletedDate,
    string Status);