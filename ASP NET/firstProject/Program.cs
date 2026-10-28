
var builder = WebApplication.CreateBuilder(args);

// Register OpenAPI services
builder.Services.AddOpenApi();

var app = builder.Build();

// Development environment
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("In the development environment....");

    // Enable OpenAPI document endpoint in development
     app.MapOpenApi();
}

app.UseHttpsRedirection();

// Define the GET endpoint
app.MapGet("/", () =>
{
    return "Hello World!";
});

app.Run();
