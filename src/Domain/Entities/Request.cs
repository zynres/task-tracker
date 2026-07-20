using System.Data.Common;
using Domain.Common.Enums;
using Domain.Common.Statics;

namespace Domain.Entities;

public class Request
{
    public int Id { get; set; }

    public int AuthorId { get; set; }
    public Employee Author { get; set; } = null!;

    public int? AssigneeId { get; private set; }
    public Employee? Assignee { get; private set; }

    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DeadLine { get; set; }

    public RequestStatus Status { get; private set; } = RequestStatus.New;
    public DateTime? CompletedDate { get; set; }

    public void Initialize(int authorId, int? assigneId, in string? description, in DateTime createdDate, in TimeSpan period, RequestStatus? status, DateTime? completedDate)
    {
        AuthorId = authorId;
        AssigneeId = assigneId;
        Description = description;
        SetDates(in createdDate, in period);
        Status = status == null ? RequestStatus.New : status.Value;
        CompletedDate = completedDate;
    }

    public void SetDates(in DateTime createdDate, in TimeSpan period)
    {
        CreatedDate = createdDate;
        DeadLine = CreatedDate + period;
    }

    public void Assign(int employeeId)
    {
        AssigneeId = employeeId;
    }

    public void ChangeStatus(RequestStatus newStatus)
    {
        if (!RequestStatusRules.CanTransitin(Status, newStatus))
            throw new InvalidOperationException();

        Status = newStatus;

        if (Status == RequestStatus.Completed)
            CompletedDate = DateTime.UtcNow;
    }
}