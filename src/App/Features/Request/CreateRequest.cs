using Domain.Common.Enums;

namespace App.Features.Request;

public record class CreateRequest(
    int AuthorId, 
    int? AssigneeId,
    string? Description, 
    TimeSpan ValidityPeriod, 
    RequestStatus? Status,
    int Count);