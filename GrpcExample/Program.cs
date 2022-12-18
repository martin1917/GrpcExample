using GrpcExample.Data;
using GrpcExample.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseInMemoryDatabase("Films"));

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<FilmService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

SeedDb(app);

app.Run();

partial class Program
{
    private static void SeedDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var ctx = services.GetRequiredService<ApplicationDbContext>();
        ContextSeed.Seed(ctx);
    }
}