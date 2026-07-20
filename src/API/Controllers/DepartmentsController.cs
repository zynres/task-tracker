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
    public async Task<ActionResult<DepartmentDto>> Create([FromBody] int count, [FromBody] string name)
    {
        var departments = new Department[count];

        for (int i = 0; i < count; i++)
        {
            var department = new Department()
            {
                Name = name
            };
            departments[i] = department;
        }

        context.Departments.AddRange(departments);

        await context.SaveChangesAsync();

        var departmentDtos = new DepartmentDto[count];

        for (int i = 0; i < count; i++)
        {
            Department department = departments[i];
            
            departmentDtos[i] = new DepartmentDto(
                department.Id, 
                department.Name);
        }

        return Ok(departmentDtos);
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