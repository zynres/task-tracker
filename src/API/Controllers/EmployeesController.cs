using Microsoft.EntityFrameworkCore;
using App.Features.Employee.Request;
using App.Features.Department.Dtos;
using App.Features.Position.Dtos;
using App.Features.Employee.Dtos;
using App.Features.Request.Dtos;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using App.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly ILogger<EmployeesController> logger;
    private readonly IDbContext context;

    public EmployeesController(ILogger<EmployeesController> logger, IDbContext context)
    {
        this.context = context;
        this.logger = logger;
    }

    [HttpPost()]
    public async Task<ActionResult<EmployeeDto[]>> Create([FromBody] EmployeeCreateRequest request)
    {
        List<Employee> employees = new(request.Count);

        var department = await context.Departments.AsNoTracking().FirstOrDefaultAsync(department => department.Id == request.DepartmentId);

        if (department == null)
            return NotFound("Department not found");

        var position = await context.Positions.AsNoTracking().FirstOrDefaultAsync(position => position.Id == request.PositionId);

        if (position == null)
            return NotFound("Position not found");

        for (int i = 0; i < request.Count; i++)
        {
            var employee = new Employee();

            employee.Initialize(
                request.Name, request.LastName, request.Patronymic,
                request.DepartmentId, request.PositionId);
            employees.Add(employee);
        }

        context.Employees.AddRange(employees);

        await context.SaveChangesAsync();

        var dtos = new EmployeeDto[request.Count];

        for (int i = 0; i < request.Count; i++)
        {
            var employee = employees[i];

            dtos[i] = new EmployeeDto(
                employee.Id,
                employee.Name,
                employee.LastName,
                employee.Patronymic,
                new DepartmentDto(
                    request.DepartmentId,
                    department.Name),
                new PositionDto(
                    request.PositionId,
                    position.Name),
                []);
        }

        return Ok(dtos);
    }

    [HttpGet("{employeeId}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployee(int employeeId)
    {
        var employeeDto = await context.Employees
            .AsNoTracking()
            .Where(employee => employee.Id == employeeId)
            .Select(employee => new EmployeeDto(
                employee.Id,
                employee.Name,
                employee.LastName,
                employee.Patronymic,
                new DepartmentDto(
                    employee.DepartmentId,
                    employee.Department!.Name),
                new PositionDto(
                    employee.PositionId,
                    employee.Position!.Name),
                employee.AssignedRequests
                    .Select(re =>
                    new RequestDto(
                        re.Id,
                        re.AuthorId,
                        re.AssigneeId,
                        re.Description, 
                        re.CreatedDate, 
                        re.DeadLine, 
                        re.CompletedDate, 
                        re.Status.ToString()))
                    .ToArray()))
            .FirstOrDefaultAsync();

        if (employeeDto == null)
            return NotFound("Employee not found");

        return Ok(employeeDto);
    }
}