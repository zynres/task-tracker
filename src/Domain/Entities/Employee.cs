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

    public List<Request> AssignedRequests { get; set; } = [];
    public List<Request> Requests { get; set; } = [];

    public void Initialize(string? name, string? lastName, string? patronymic, int departmentId, int positionId)
    {
        Name = name;
        LastName = lastName;
        Patronymic = patronymic;
        DepartmentId = departmentId;
        PositionId = positionId;
    }
}