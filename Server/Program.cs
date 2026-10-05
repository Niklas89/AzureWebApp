// Support starting from either the server directory or the solution directory.
var envPath = File.Exists(".env") ? ".env" : Path.Combine("Server", ".env");
DotNetEnv.Env.Load(envPath);
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var apiKey = Environment.GetEnvironmentVariable("API_KEY");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

Console.WriteLine($"Running in: {environment}");

Console.WriteLine($"Using API Key: {apiKey}");

// Azure App Service terminates TLS. Enable HTTPS Only in its configuration.
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapHealthChecks("/health");
app.MapFallbackToFile("index.html");

app.Run();
