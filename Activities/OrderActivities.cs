using Temporalio.Activities;

namespace TemporalDemo.Activities;

public interface IOrderActivities
{
    [Activity]
    Task<string> SayHelloAsync(string name);
    
    [Activity]
    Task<string> SayGoodbyeAsync(string name);
}

public class OrderActivities : IOrderActivities
{
    [Activity]
    public async Task<string> SayHelloAsync(string name)
    {
        // Simple activity that just returns a greeting
        await Task.Delay(1000); // Simulate some work
        // throw new ApplicationException("Simulated error in activity!");
        return $"Hello, {name}! Time: {DateTime.Now:HH:mm:ss}";
    }

    [Activity]
    public async Task<string> SayGoodbyeAsync(string name)
    {
        // Simple activity that just returns a goodbye
        await Task.Delay(1000); // Simulate some work
        // throw new ApplicationException("Simulated error in activity!");
        return $"Goodbye, {name}! Time: {DateTime.Now:HH:mm:ss}";
    }
}
