using Aspose.Cells.Charts;
using EventRun_Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var urlCors = builder.Configuration.GetSection("AppSettings:Cors").Value!;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins", builder =>
    {
        builder.WithOrigins(urlCors).AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddDbContext<EventRunContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectiion"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAngularOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
