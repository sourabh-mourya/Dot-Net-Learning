// App ka setup shuru. args = command line arguments (config, logging, DI yahin se ready hote hain)
var builder = WebApplication.CreateBuilder(args);

// OpenAPI (API docs) ko register karo
builder.Services.AddOpenApi();

// App banao. Iske baad services add nahi hoti, sirf middleware/routes lagte hain
var app = builder.Build();

// Sirf development mode me chalega (NODE_ENV jaisa)
if (app.Environment.IsDevelopment())
{
    // Start hone par ek baar print hoga, har request pe nahi
    Console.WriteLine("In the development environment....");

    // API docs JSON ka endpoint: GET /openapi/v1.json
    app.MapOpenApi();
}

// HTTP request ko HTTPS pe redirect karo
app.UseHttpsRedirection();

// GET / route. Express: app.get('/', ...)
app.MapGet("/", () =>
{
    // String return karoge to plain text response (200 OK) jayega
    return "Hello World!";
});

// Server start karo (blocking call). Express: app.listen()
app.Run();