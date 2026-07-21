using NBomber.Http.CSharp;
using NBomber.CSharp;

namespace LoadTest;

public class Program
{
    public static void Main(string[] args)
    {
        var client = Http.CreateDefaultClient();

        var scenario = Scenario.Create("Test Performance", async context =>
        {
            var request = Http.CreateRequest(
                    "GET",                        // change the assignee id to your
                    "http://localhost:5100/api/requests/filter?assigneeId=1000&status=InProgress&isOverdue=true");
            
            return await Http.Send(client, request);
        })
        .WithWarmUpDuration(TimeSpan.FromSeconds(5))
        .WithLoadSimulations(Simulation.Inject(
            rate: 10,
            interval: TimeSpan.FromSeconds(1),
            during: TimeSpan.FromSeconds(30)));

        NBomberRunner
            .RegisterScenarios(scenario)
            .Run();
    }
}
