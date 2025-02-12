using Temporalio.Workflows;
using TemporalDemo.Activities;

namespace TemporalDemo.Workflows;

[Workflow]
public class OrderWorkflow
{
    [WorkflowRun]
    public async Task<string> RunAsync(string name)
    {
        // Execute a simple greeting activity
        var greeting = await Workflow.ExecuteActivityAsync(
            (IOrderActivities act) => act.SayHelloAsync(name),
            new() { ScheduleToCloseTimeout = TimeSpan.FromMinutes(1) });
       
        var goodbye = await Workflow.ExecuteActivityAsync(
            (IOrderActivities act) => act.SayGoodbyeAsync(name),
            new() { ScheduleToCloseTimeout = TimeSpan.FromMinutes(1) });

        return $"Workflow completed with greeting: {greeting} and goodbye: {goodbye}";
    }
}
