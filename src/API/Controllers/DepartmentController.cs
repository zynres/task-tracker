using Microsoft.AspNetCore.Mvc;
using App.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/department")]
public class DepartmentController : ControllerBase
{
    private readonly ILogger<DepartmentController> logger;
    private readonly IDbContext context;

    public DepartmentController(ILogger<DepartmentController> logger, IDbContext context)
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