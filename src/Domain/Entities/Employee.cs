namespace Domain.Entities;

public class Employee
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Patronymic { get; set; }

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    public int PositionId { get; set; }
    public Position? Position { get; set; }

    public int? AssignetRequestId { get; set; }
    public Request? AssignetRequest { get; set; }
    public List<Request> Requests = [];
}