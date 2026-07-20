using App.Features.Department.Handlers;
using App.Features.Employee.Handlers;
using App.Features.Position.Handlers;
using App.Features.Request.Handlers;
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

        builder.Services.AddScoped<CreateEmployeesHandler>();
        builder.Services.AddScoped<GetEmployeeHandler>();

        builder.Services.AddScoped<CreateDepartmentHandler>();
        builder.Services.AddScoped<GetDepartmentHandler>();

        builder.Services.AddScoped<CreatePositionHandler>();
        builder.Services.AddScoped<GetPositionHandler>();

        builder.Services.AddScoped<GetRequestsByFilterHandler>();
        builder.Services.AddScoped<ChangeAssigneeHandler>();
        builder.Services.AddScoped<CreateRequestsHandler>();
        builder.Services.AddScoped<ChangeStatusHandler>();
        builder.Services.AddScoped<GetReportsHandler>();

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