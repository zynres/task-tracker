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

    public required string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DeadLine { get; set; }

    public RequestStatus Status { get; private set; } = RequestStatus.New;

    public void Assign(in Employee employee)
    {
        if (AssigneeId != default)
            throw new Exception("Request already has a assignee.");

        Assignee = employee;
    }

    public void ChangeStatus(RequestStatus newStatus)
    {
        if (!RequestStatusRules.CanTransitin(Status, newStatus))
            throw new InvalidOperationException();

        Status = newStatus;
    }
}