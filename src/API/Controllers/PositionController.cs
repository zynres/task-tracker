using Microsoft.EntityFrameworkCore;
using App.Features.Position.Dtos;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
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
    public async Task<ActionResult<PositionDto>> Create(int count, [FromBody] string name)
    {
        var positions = new Position[count];

        for (int i = 0; i < count; i++)
        {
            var position = new Position()
            {
                Name = name
            };
            positions[i] = position;
        }

        context.Positions.AddRange(positions);

        await context.SaveChangesAsync();

        var positionDtos = new PositionDto[count];

        for (int i = 0; i < count; i++)
        {
            Position position = positions[i];
            
            positionDtos[i] = new PositionDto(
                position.Id, 
                position.Name);
        }

        return Ok(positionDtos);
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