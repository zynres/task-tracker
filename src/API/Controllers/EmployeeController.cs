using Microsoft.AspNetCore.Mvc;
using App.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/employee")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<EmployeeController> logger;
    private readonly IDbContext context;

    public EmployeeController(ILogger<EmployeeController> logger, IDbContext context)
    {
        this.context = context;
        this.logger = logger;
    }

    [HttpPost("count/{count}")]
    public async Task<IActionResult> Create(int count)
    {
        return Ok(count);
    }
}