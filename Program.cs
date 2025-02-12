using Temporalio.Client;
using Temporalio.Worker;
using TemporalDemo.Activities;
using TemporalDemo.Workflows;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Temporal Client
ITemporalClient client;
if (builder.Environment.IsProduction())
{
    // Production: Temporal Cloud Configuration
    /*
    var clientOptions = new TemporalClientOptions
    {
        // Replace with your Temporal Cloud namespace address
        TargetHost = "your-namespace.tmprl.cloud:7233",
        Namespace = "your-namespace",
        
        // Replace these with paths to your certificate files
        ClientCert = File.ReadAllBytes("path/to/client.pem"),
        ClientKey = File.ReadAllBytes("path/to/client.key"),

        // Enable TLS for Cloud connection
        Tls = true
    };
    client = await TemporalClient.ConnectAsync(clientOptions);
    */
    
    // Comment out the above and remove this line when cloud config is ready
    client = await TemporalClient.ConnectAsync(new("localhost:7233"));
}
else
{
    // Development: Local Configuration
    client = await TemporalClient.ConnectAsync(new("localhost:7233"));
}

builder.Services.AddSingleton<ITemporalClient>(client);

// Configure cancellation token for worker shutdown
var workerCts = new CancellationTokenSource();
builder.Services.AddSingleton(workerCts);

// Configure and start Temporal worker
var activities = new OrderActivities();
var worker = new TemporalWorker(
    client,
    new TemporalWorkerOptions("order-tasks")
        .AddAllActivities(activities)
        .AddWorkflow<OrderWorkflow>());

// Start the worker in the background
_ = Task.Run(async () => 
{
    try
    {
        Console.WriteLine("Starting Temporal worker...");
        await worker.ExecuteAsync(workerCts.Token);
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Worker cancelled");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Worker error: {ex}");
    }
});

var app = builder.Build();

// Configure graceful shutdown
app.Lifetime.ApplicationStopping.Register(() => 
{
    Console.WriteLine("Shutting down worker...");
    workerCts.Cancel();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
