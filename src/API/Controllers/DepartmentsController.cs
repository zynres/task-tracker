using Microsoft.EntityFrameworkCore;
using App.Features.Department.Dtos;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using App.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly ILogger<DepartmentsController> logger;
    private readonly IDbContext context;

    public DepartmentsController(ILogger<DepartmentsController> logger, IDbContext context)
    {
        this.context = context;
        this.logger = logger;
    }

    [HttpPost()]
    public async Task<ActionResult<DepartmentDto>> Create([FromBody] string name)
    {
        var department = new Department()
        {
            Name = name
        };

        context.Departments.Add(department);

        await context.SaveChangesAsync();

        return Ok(new DepartmentDto(
                department.Id,
                department.Name));
    }

    [HttpGet("{departmentId}")]
    public async Task<ActionResult<DepartmentDto>> GetDepartment(int departmentId)
    {
        var departmentDto = await context.Departments
            .AsNoTracking()
            .Where(department => department.Id == departmentId)
            .Select(department => new DepartmentDto(department.Id, department.Name))
            .FirstOrDefaultAsync();

        if (departmentDto == null)
            return NotFound("Department not found");

        return Ok(departmentDto);
    }
}