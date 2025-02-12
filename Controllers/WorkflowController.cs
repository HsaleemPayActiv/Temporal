using Microsoft.AspNetCore.Mvc;
using Temporalio.Client;
using TemporalDemo.Workflows;

namespace TemporalDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkflowController : ControllerBase
{
    private readonly ITemporalClient _client;

    public WorkflowController(ITemporalClient client)
    {
        _client = client;
    }

    // [HttpPost("start-order")]
    // public async Task<IActionResult> StartOrder([FromBody] string orderId)
    // {
    //     // Start the workflow
    //     var handle = await _client.StartWorkflowAsync(
    //         (OrderWorkflow wf) => wf.RunAsync(orderId),
    //         new WorkflowOptions
    //         {
    //             Id = $"order-workflow-{orderId}",
    //             TaskQueue = "order-tasks"
    //         });

    //     // Return the workflow ID
    //     return Ok(new { WorkflowId = handle.Id });
    // }

    // [HttpGet("order-status/{workflowId}")]
    // public async Task<IActionResult> GetOrderStatus(string workflowId)
    // {
    //     try
    //     {
    //         var handle = _client.GetWorkflowHandle(workflowId);
    //         var result = await handle.GetResultAsync<string>();
    //         return Ok(new { Status = "Completed", Result = result });
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, new { Error = ex.Message });
    //     }
    // }

    [HttpPost("greet")]
    public async Task<IActionResult> Greet([FromBody] string name)
    {
        // Start the workflow
        var handle = await _client.StartWorkflowAsync(
            (OrderWorkflow wf) => wf.RunAsync(name),
            new WorkflowOptions
            {
                Id = $"greeting-{Guid.NewGuid()}",
                TaskQueue = "order-tasks"
            });

        // Return the workflow ID
        return Ok(new { WorkflowId = handle.Id });
    }

    [HttpGet("greeting-status/{workflowId}")]
    public async Task<IActionResult> GetGreetingStatus(string workflowId)
    {
        try
        {
            var handle = _client.GetWorkflowHandle(workflowId);
            var result = await handle.GetResultAsync<string>();
            return Ok(new { Status = "Completed", Result = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = ex.Message });
        }
    }
}
