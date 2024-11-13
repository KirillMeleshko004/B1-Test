using ExcelServer.Api.Extensions;
using ExcelServer.Controllers;
using ExcelServer.Infrastructure;
using ExcelServer.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors();
builder.Services.ConfigureLogging();

builder.Services.ConfigurePresentation();
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

var app = builder.Build();

#region Configure app pipeline

var logger = app.Services.GetRequiredService<ILogger<Program>>();
app.ConfigureExceptionHandler(logger);

app.UseCors("default");

app.MapControllers();

#endregion

app.Run();