
var builder = WebApplication.CreateBuilder(args);

// MVC Controllers aur Views ko register karo
builder.Services.AddControllersWithViews();

var app = builder.Build();

// GET endpoint
app.MapGet("/", () =>
{
    return "Welcome to Student API! - GET";
});

// GET + HttpContext + async/await
app.MapGet("/hello", async (HttpContext context) =>
{
    context.Response.ContentType = "text/plain";

    await context.Response.WriteAsync(
        "Hello! Response sent using HttpContext. - GET"
    );
});

// PUT endpoint
app.MapPut("/students/{id:int}", async (int id, HttpContext context) =>
{
    await context.Response.WriteAsync(
        $"Student {id} updated using PUT."
    );
});

// POST endpoint
app.MapPost("/students", async (HttpContext context) =>
{
    await context.Response.WriteAsync(
        "Student created using POST."
    );
});

// Controller ke attribute routes register karo
app.MapControllers();

app.Run();
