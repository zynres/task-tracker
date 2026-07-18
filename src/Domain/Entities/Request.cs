using Domain.Common.Enums;
using Domain.Common.Statics;

namespace Domain.Entities;

public class Request
{
    public int Id { get; set; }

    public int AuthorId { get; set; }
    public Employee Author { get; set; } = null!;

    public int? PerformerId { get; private set; }
    public Employee? Performer { get; private set; }

    public int Number { get; set; }
    public required string Title { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DeadLine { get; set; }

    public RequestStatus Status { get; private set; } = RequestStatus.New;

    public void Assign(in Employee employee)
    {
        if (PerformerId != default)
            throw new Exception("Request already has a performer.");

        Performer = employee;
    }

    public void ChangeStatus(RequestStatus newStatus)
    {
        if (!RequestStatusRules.CanTransitin(Status, newStatus))
            throw new InvalidOperationException();

        Status = newStatus;
    }
}