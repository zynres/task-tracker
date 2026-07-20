using Microsoft.EntityFrameworkCore;
using App.Features.Position.Dtos;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using App.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PositionsController : ControllerBase
{
    private readonly ILogger<PositionsController> logger;
    private readonly IDbContext context;

    public PositionsController(ILogger<PositionsController> logger, IDbContext context)
    {
        this.context = context;
        this.logger = logger;
    }

    [HttpPost()]
    public async Task<ActionResult<PositionDto>> Create([FromBody] string name)
    {
        var position = new Position()
        {
            Name = name
        };

        context.Positions.Add(position);

        await context.SaveChangesAsync();

        return Ok(new PositionDto(
                position.Id,
                position.Name));
    }

    [HttpGet("{positionId}")]
    public async Task<ActionResult<PositionDto>> GetPosition(int positionId)
    {
        var positionDto = await context.Positions
            .AsNoTracking()
            .Where(position => position.Id == positionId)
            .Select(position => new PositionDto(position.Id, position.Name))
            .FirstOrDefaultAsync();

        if (positionDto == null)
            return NotFound("Position not found");

        return Ok(positionDto);
    }
}