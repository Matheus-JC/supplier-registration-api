using Api.Configurations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using SupplierRegServer.Api.Configurations;
using SupplierRegServer.Data.Context;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("DefaultConnections")
    ?? throw new Exception("connection string section not found");

// Add services to the container

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connString);
});

builder.Services.AddLogConfiguration();

builder.Services.AddIdentityConfig(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddApiConfig();

builder.Services.AddSwaggerConfig();

builder.Services.AddHealthChecks()
    .AddSqlServer(connString, name: "MSSQLDB");

builder.Services.ResolveDependencies();

var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline

app.UseLogConfig(builder.Configuration);

app.UseApiConfig(app.Environment);

app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.UseHealthChecks("/api/hc");

app.Run();
