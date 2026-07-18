using Microsoft.AspNetCore.Mvc;
using App.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/request")]
public class RequestController : ControllerBase
{
    private readonly ILogger<RequestController> logger;
    private readonly IDbContext context;

    public RequestController(ILogger<RequestController> logger, IDbContext context)
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