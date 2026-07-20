using Domain.Common.Enums;

namespace App.Features.Request;

public record class RequestFilter(
    int? AssigneeId, 
    RequestStatus? Status, 
    int? DepartmentId, 
    bool? IsOverdue);