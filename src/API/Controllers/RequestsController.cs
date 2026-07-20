using Microsoft.EntityFrameworkCore;
using App.Features.Employee.Dtos;
using App.Features.Request.Dtos;
using Microsoft.AspNetCore.Mvc;
using App.Features.Request;
using Domain.Common.Enums;
using Domain.Entities;
using App.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RequestsController : ControllerBase
{
    private readonly ILogger<RequestsController> logger;
    private readonly IDbContext context;

    public RequestsController(ILogger<RequestsController> logger, IDbContext context)
    {
        this.context = context;
        this.logger = logger;
    }

    [HttpPost()]
    public async Task<ActionResult<RequestDto[]>> Create([FromBody] CreateRequest command)
    {
        var author = await context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(employee => employee.Id == command.AuthorId);

        if (author == null)
            return NotFound("Employee not found");

        int? assigneeId = command.AssigneeId;

        if (assigneeId != null && !await context.Employees.AnyAsync(employee => employee.Id == assigneeId))
            assigneeId = null;

        List<Request> requests = new(command.Count);

        for (int i = 0; i < command.Count; i++)
        {
            var request = new Request();

            request.Initialize(
                command.AuthorId, assigneeId, command.Description,
                DateTime.UtcNow, command.ValidityPeriod,
                command.Status, null);
            requests.Add(request);
        }

        context.Requests.AddRange(requests);

        await context.SaveChangesAsync();

        var dtos = new RequestDto[command.Count];

        for (int i = 0; i < command.Count; i++)
        {
            var request = requests[i];

            dtos[i] = new RequestDto(
                request.Id, 
                request.AuthorId, 
                request.AssigneeId,
                request.Description, 
                request.CreatedDate, 
                request.DeadLine,
                request.CompletedDate,
                request.Status.ToString());
        }

        return Ok(dtos);
    }

    [HttpPatch("{requestId}")]
    public async Task<ActionResult<RequestDto>> ChangeStatus(int requestId, [FromBody] RequestStatus status)
    {
        var request = await context.Requests
            .FirstOrDefaultAsync(request => request.Id == requestId);
        
        if (request == null)
            return NotFound("Request not found");

        request.ChangeStatus(status);

        await context.SaveChangesAsync();

        return Ok(new RequestDto(
            request.Id, 
            request.AuthorId, 
            request.AssigneeId, 
            request.Description, 
            request.CreatedDate, 
            request.DeadLine, 
            request.CompletedDate, 
            request.Status.ToString()));
    }

    [HttpPatch("{requestId}/assignee/{assigneeId}")]
    public async Task<IActionResult> ChangeAssignee(int requestId, int assigneeId)
    {
        var request = await context.Requests.FirstOrDefaultAsync(request => request.Id == requestId);
        
        if (request == null)
            return NotFound("Request not found");

        if (!await context.Employees.AnyAsync(employee => employee.Id == assigneeId))
            return NotFound("Employee not found");

        request.Assign(assigneeId);

        await context.SaveChangesAsync();

        return Ok(new RequestDto(
            request.Id, 
            request.AuthorId, 
            request.AssigneeId, 
            request.Description, 
            request.CreatedDate, 
            request.DeadLine, 
            request.CompletedDate, 
            request.Status.ToString()));
    }

    [HttpGet("filter")]
    public async Task<ActionResult<RequestDto[]>> GetRequests([FromQuery] RequestFilter filter)
    {
        IQueryable<Request> query = context.Requests.AsNoTracking();

        if (filter.AssigneeId != null)
        {
            query = query.Where(request => request.AssigneeId == filter.AssigneeId);
        }
        if (filter.Status != null)
        {
            query = query.Where(request => request.Status == filter.Status);
        }
        if (filter.DepartmentId != null)
        {
            query = query.Where(request => 
                request.AssigneeId != null &&
                request.Assignee!.DepartmentId == filter.DepartmentId);
        }
        if (filter.IsOverdue != null) 
        {
            if (filter.IsOverdue.Value)
            {
                query = query
                    .Where(request => 
                        DateTime.UtcNow > request.DeadLine)
                    .OrderBy(request => request.DeadLine); 
                // for test: Retrieve all overdue requests assigned to a specific employee 
                // that are currently **In Progress**, ordered by due date.
            }
            else
            {
                query = query
                    .Where(request => 
                        DateTime.UtcNow <= request.DeadLine)
                    .OrderBy(request => request.DeadLine); 
            }
        }

        var requests = await query
            .Select(request => new RequestDto(
                request.Id, 
                request.AuthorId, 
                request.AssigneeId, 
                request.Description, 
                request.CreatedDate, 
                request.DeadLine, 
                request.CompletedDate, 
                request.Status.ToString()))
            .ToArrayAsync();

        return Ok(requests);
    }

    [HttpGet("report")]
    public async Task<ActionResult<RequestReportDto>> GetReports()
    {
        var report = await context.Requests
            .AsNoTracking()
            .GroupBy(request => request.Status)
            .Select(request =>
                new
                {
                    request.Key,
                    Count = request.Count()
                })
            .ToDictionaryAsync(
                g => g.Key, 
                g => g.Count);

        int overduesCount = await context.Requests
            .AsNoTracking()
            .CountAsync(request => 
                (DateTime.UtcNow > request.DeadLine && request.Status != RequestStatus.Completed) || 
                request.CompletedDate > request.DeadLine);

        var employees = await context.Requests
            .AsNoTracking()
            .Where(request =>
                request.AssigneeId != null &&
                request.Status == RequestStatus.Completed)
            .GroupBy(request => new
            {
                request.AssigneeId,
                request.Assignee!.Name
            })
            .Select(g =>
                new CompletedByEmployeeDto(
                    g.Key.AssigneeId!.Value,
                    g.Key.Name,
                    g.Count()))
            .ToArrayAsync();

        return Ok(new RequestReportDto(
            report[RequestStatus.New],
            report[RequestStatus.InProgress], 
            report[RequestStatus.Completed], 
            overduesCount, employees));
    }
}