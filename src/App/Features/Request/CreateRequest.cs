using Domain.Common.Enums;

namespace App.Features.Request;

public record class CreateRequest(
    string? Description, 
    TimeSpan ValidityPeriod, 
    RequestStatus? Status);