using Microsoft.EntityFrameworkCore;
using Oficina.Application.Interfaces;
using Oficina.Application.Services;
using Oficina.Infrastructure.Data;
using Oficina.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQL Server
builder.Services.AddDbContext<OficinaDbContext>(options =>
    options.UseInMemoryDatabase("OficinaDB"));
    //options.UseSqlServer(builder.Configuration.GetConnectionString("OficinaDb")));

// Repositories (Infrastructure)
builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();

// Application services
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IAreaService, AreaService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
