WebApplication.CreateBuilder(args) — creates the application builder and loads configuration.



builder.Services.AddOpenApi() — registers services for generating an OpenAPI document.



builder.Build() — builds the application.



app.Environment.IsDevelopment() — checks whether the environment is Development.



app.MapOpenApi() — exposes the generated OpenAPI document endpoint.



app.UseHttpsRedirection() — redirects eligible HTTP requests to HTTPS.



app.MapGet() — defines a GET endpoint.



app.Run() — starts the web server.