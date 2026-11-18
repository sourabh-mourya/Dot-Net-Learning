using Hybrid_Caching.Data;
using Hybrid_Caching.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Redis Distributed Cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration
        .GetConnectionString("RedisConnection");

    options.InstanceName = "HybridCaching_";
});

// Hybrid Cache
builder.Services.AddHybridCache();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// MySQL registration remains the same
// builder.Services.AddDbContext<ApplicationDbContext>(...);

var app = builder.Build();

app.MapControllers();

app.Run();