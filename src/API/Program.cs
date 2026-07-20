using App.Interfaces;
using Inf.Context;
using Inf.Services;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<TaskTrackerContext>(options => 
            options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));
        builder.Services.AddScoped<IDbContext>(provider => 
            provider.GetRequiredService<TaskTrackerContext>());

        builder.Services.AddScoped<DataSeeder>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            await using var scope = app.Services.CreateAsyncScope();

            var context = scope.ServiceProvider.GetRequiredService<TaskTrackerContext>();
            context.Database.Migrate();

            var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
            
            await dataSeeder.Seed();
        }

        app.UseRouting();
        app.MapControllers();

        app.Run();
    }
}