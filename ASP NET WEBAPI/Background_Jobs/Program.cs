using Background_Jobs.Filters;
using Background_Jobs.Jobs;
using Background_Jobs.Services;
using Hangfire;
using Hangfire.MySql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// MySQL connection string (appsettings.json se)
var connectionString = builder.Configuration.GetConnectionString("HangfireConnection")
    ?? throw new InvalidOperationException("ConnectionStrings:HangfireConnection missing hai.");

// Logging filter (har job ka start/success/fail console me)
builder.Services.AddSingleton<JobLoggingFilter>();

// Hangfire configuration
builder.Services.AddHangfire((serviceProvider, config) => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseFilter(serviceProvider.GetRequiredService<JobLoggingFilter>())
    .UseStorage(new MySqlStorage(connectionString, new MySqlStorageOptions
    {
        TablesPrefix = "Hangfire",          // tables: Hangfire_Job, Hangfire_State ...
        PrepareSchemaIfNecessary = true,    // tables khud ban jayengi
        QueuePollInterval = TimeSpan.FromSeconds(5)
    })));

// Hangfire background server (jobs yahi execute karta hai)
builder.Services.AddHangfireServer(options =>
{
    options.ServerName = "Background_Jobs_Server";
    options.WorkerCount = Math.Max(1, Environment.ProcessorCount * 2);
    options.Queues = new[] { "default" };
});

// Apni service + job classes DI me register
builder.Services.AddScoped<IBackroundService, HanfireBackgroundJobService>();
builder.Services.AddTransient<EmailJob>();
builder.Services.AddTransient<ReportJob>();
builder.Services.AddTransient<FailingJob>();                      

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Dashboard: /hangfire
// Default me sirf localhost se khulta hai. Production me authorization filter zaroor lagana.
app.UseHangfireDashboard("/hangfire");

app.MapControllers();

// Startup par recurring jobs register
using (var scope = app.Services.CreateScope())
{
    var bg = scope.ServiceProvider.GetRequiredService<IBackroundService>();

    // Har minute (demo - console me logs dikhte rahenge)
    bg.AddOrUpdateRecurring<ReportJob>("heartbeat-job", j => j.HeartbeatAsync(), Cron.Minutely());

    // Roz midnight UTC
    bg.AddOrUpdateRecurring<ReportJob>("daily-employee-report", j => j.GenerateDailyReportAsync(), Cron.Daily());
}

app.Run();
