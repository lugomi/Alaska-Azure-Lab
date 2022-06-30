using LabService.Models;
using LabService.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Azure Lab Service",
        Description = "This is a simple service for demoing azure functionality",
        Version = "v1"
    });
});

builder.Services.AddTransient<IPeopleRepository, PeopleRepository>();
builder.Services.AddSingleton(builder.Configuration.GetSection("database").Get<DatabaseConfiguration>() ?? new DatabaseConfiguration());

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Azure Lab Service V1");
    options.RoutePrefix = string.Empty;
});
app.MapControllers();

app.Run();
