using Microsoft.AspNetCore.Mvc;
using App.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/position")]
public class PositionController : ControllerBase
{
    private readonly ILogger<PositionController> logger;
    private readonly IDbContext context;

    public PositionController(ILogger<PositionController> logger, IDbContext context)
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