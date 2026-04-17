using Microsoft.EntityFrameworkCore;
using PassGeneratorService.Application.Interfaces;
using PassGeneratorService.Application.Settings;
using PassGeneratorService.Domain.Services;
using PassGeneratorService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.Configure<PasswordSettings>(builder.Configuration.GetSection("PasswordSettings"));
builder.Services.AddScoped<IGeneratorService, GeneratorService>();
builder.Services.AddScoped<IPasswordRepository, PasswordRepository>();
builder.Services.AddScoped<AppDbContext, AppDbContext>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
