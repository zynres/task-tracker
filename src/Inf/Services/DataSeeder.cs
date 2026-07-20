using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Common.Enums;
using Domain.Entities;
using App.Interfaces;

namespace Inf.Services;

public class DataSeeder
{
    private const int EmployeesCount = 1000;
    private const int RequestsCount = 1_000_000;

    private readonly ILogger<DataSeeder> logger;
    private readonly IDbContext context;

    public DataSeeder(IDbContext context, ILogger<DataSeeder> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task Seed()
    {
        if (await context.Employees.AnyAsync())
        {
            logger.LogInformation("Has data");
            return;
        }

        int departmentsCount = await context.Departments.CountAsync();
        int positionsCount = await context.Positions.CountAsync();

        if (departmentsCount == 0 || positionsCount == 0)
        {
            logger.LogError("Database don't have departments or positions");
            return;
        }

        logger.LogInformation("Generating employees...");

        string[] names =
        [
            "John","Mike","Alex","David","Chris","Daniel","James","Andrew",
            "Robert","Mark","Michael","William","Richard","Joseph","Thomas",
            "Anthony","Charles","Steven","Kevin","Brian"
        ];

        string[] lastNames =
        [
            "Smith","Johnson","Williams","Brown","Jones","Garcia","Miller",
            "Davis","Wilson","Taylor","Moore","Jackson","Martin","Lee",
            "Walker","Hall","Allen","Young","King","Scott"
        ];

        string[] patronymics =
        [
            "Ivanovich",
            "Petrovich",
            "Alexandrovich",
            "Nikolaevich",
            "Sergeevich"
        ];

        var employees = new Employee[EmployeesCount];

        for (int i = 0; i < EmployeesCount; i++)
        {
            var employee = new Employee();

            employee.Initialize(
                names[Random.Shared.Next(names.Length)],
                lastNames[Random.Shared.Next(lastNames.Length)],
                patronymics[Random.Shared.Next(patronymics.Length)],
                Random.Shared.Next(1, departmentsCount + 1),
                Random.Shared.Next(1, positionsCount + 1)
                );

            employees[i] = employee;
        }

        context.Employees.AddRange(employees);

        await context.SaveChangesAsync();

        List<int> fewTasks = [];
        List<int> manyTasks = [];

        for (int i = 0; i < employees.Length; i++)
        {
            int random = Random.Shared.Next(100);
            var employee = employees[i];

            if (random < 20)
                continue;
            if (random < 40)
                fewTasks.Add(employee.Id);
            else
                manyTasks.Add(employee.Id);
        }

        logger.LogInformation($"Employees didn't have tasks: {employees.Length - (fewTasks.Count + manyTasks.Count)}");
        logger.LogInformation($"Employees haved few tasks: {fewTasks.Count}");
        logger.LogInformation($"Employees haved many tasks: {manyTasks.Count}");

        int createdRequests = 0;
        const int BatchSize = 5000;

        List<Request> requests = new(BatchSize);

        logger.LogInformation("Generating requests...");

        foreach (int employeeId in fewTasks)
        {
            int count = Random.Shared.Next(1, 4);

            for (int i = 0; i < count; i++)
            {
                requests.Add(CreateRandomRequest(
                    employees[Random.Shared.Next(employees.Length)].Id, 
                    employeeId));

                createdRequests++;

                if (requests.Count >= BatchSize)
                {
                    context.Requests.AddRange(requests);

                    await context.SaveChangesAsync();

                    context.GetChangeTracker().Clear();

                    logger.LogInformation($"{createdRequests:N0} created requests...");

                    requests.Clear();
                }
            }
        }

        while (createdRequests < RequestsCount)
        {
            int? assigneeId = null;

            // 60% assigned
            if (Random.Shared.NextDouble() >= 0.40)
            {
                assigneeId = manyTasks[
                    Random.Shared.Next(manyTasks.Count)];
            }

            requests.Add(CreateRandomRequest(
                employees[Random.Shared.Next(employees.Length)].Id, 
                assigneeId));

            createdRequests++;

            if (requests.Count >= BatchSize)
            {
                context.Requests.AddRange(requests);

                await context.SaveChangesAsync();

                context.GetChangeTracker().Clear();

                logger.LogInformation($"{createdRequests:N0} created requests...");

                requests.Clear();
            }
        }

        if (requests.Count > 0)
        {
            context.Requests.AddRange(requests);

            await context.SaveChangesAsync();

            context.GetChangeTracker().Clear();
        }

        logger.LogInformation("Finished.");
    }

    private static Request CreateRandomRequest(int authorId, int? assigneeId)
    {
        Request request = new();

        RequestStatus? status = null;
        DateTime? completedDate = null;
        DateTime createdDate = DateTime.UtcNow.AddDays(-Random.Shared.Next(61));

        if (assigneeId != null)
        {
            if (Random.Shared.Next(2) == 0)
            {
                status = RequestStatus.InProgress;
            }
            else
            {
                status = RequestStatus.Completed;

                completedDate = createdDate.AddDays(Random.Shared.Next(1, 61));
            }
        }

        request.Initialize(
            authorId,
            assigneeId,
            $"Request: the test request don't have any task",
            createdDate,
            TimeSpan.FromDays(Random.Shared.Next(1, 61)),
            status,
            completedDate);

        return request;
    }
}