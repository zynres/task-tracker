using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Inf.Context;

public class ContextFactory : IDesignTimeDbContextFactory<TaskTrackerContext>
{
    public TaskTrackerContext CreateDbContext(string[] args)
    {
        string pathBase = Path.Combine(Directory.GetCurrentDirectory(), "../API");

        var config = new ConfigurationBuilder()
            .SetBasePath(pathBase)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TaskTrackerContext>();

        optionsBuilder.UseNpgsql(config.GetConnectionString("Db"));

        return new TaskTrackerContext(optionsBuilder.Options);
    }
}