using App.Interfaces;
using Inf.Context;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Program
{
    public static void Main(string[] args)
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

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        app.Run();
    }
}