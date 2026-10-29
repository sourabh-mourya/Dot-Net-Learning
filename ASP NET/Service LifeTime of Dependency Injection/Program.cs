using DependencyInjectionServices.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Register the service with Scoped lifetime
// builder.Services.AddScoped<IService1, ServiceImplementation>();
// builder.Services.AddSingleton<IService1, ServiceImplementation>();
builder.Services.AddTransient<IService1, ServiceImplementation>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();