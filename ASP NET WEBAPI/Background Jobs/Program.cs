
using Hangfire;
using Hangfire.MySql;
using Background_Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddOpenApi();

// Get MySQL connection string from appsettings.json
var connectionString = builder.Configuration
    .GetConnectionString("HangfireConnection");

// Configure Hangfire
builder.Services.AddHangfire(config => config

    // Defines Hangfire data compatibility version
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)

    // Uses simple assembly names for serialization
    .UseSimpleAssemblyNameTypeSerializer()

    // Uses recommended serialization settings
    .UseRecommendedSerializerSettings()

    // Configure MySQL as Hangfire job storage
    .UseStorage(new MySqlStorage(connectionString))
);

// Register Hangfire Background Job Server
builder.Services.AddHangfireServer(options =>
{
    // Number of background workers
    options.WorkerCount = Math.Max(
        1,
        Environment.ProcessorCount * 2
    );

    // Jobs will be picked from these queues
    options.Queues = new[] { "default" };
});

// Register your custom background service implementation here,
// if you have created its concrete class.
// builder.Services.AddScoped<IBackroundService, BackgroundServiceImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Hangfire Dashboard
app.UseHangfireDashboard("/hangfire");

app.MapControllers();

app.Run();
