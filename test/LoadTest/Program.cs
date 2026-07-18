using NBomber.Http.CSharp;
using NBomber.CSharp;

namespace LoadTest;

public class Program
{
    public static void Main(string[] args)
    {
        var client = Http.CreateDefaultClient();

        var scenario =
            Scenario.Create("Post Employees", async context =>
            {
                var request = Http.CreateRequest(
                    "POST",
                    "http://localhost:5100/api/employee/count/1");

                return await Http.Send(client, request);
            })
            .WithLoadSimulations(
                Simulation.Inject(
                    rate: 10000,
                    interval: TimeSpan.FromSeconds(1),
                    during: TimeSpan.FromSeconds(30))
            );

        NBomberRunner
            .RegisterScenarios(scenario)
            .Run();
    }
}
