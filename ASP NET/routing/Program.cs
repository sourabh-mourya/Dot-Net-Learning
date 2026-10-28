
var builder = WebApplication.CreateBuilder(args);

// Register MVC controllers and views
builder.Services.AddControllersWithViews();

// Register OpenAPI
// builder.Services.AddOpenApi();

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRouting();

// Configure conventional routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
