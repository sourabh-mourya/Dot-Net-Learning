var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


// app.Run(async (context) =>
// {
//     await context.Response.WriteAsync("These is a first middle ware....");
// });


app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("These is a first middle ware....\n");
    await next(context);
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync("These is a second middle ware....");
});
//iske aage ki koi bhi request ya middleware chlega hi nhi 

//agar apne / routes pr bhi jye to wo bhi nhi chlega....
//kyuki apna Run middleware ka used kr le rhe hi isliye................


app.MapGet("/", () =>
{
    return "Hello World!";
});


app.Run();
