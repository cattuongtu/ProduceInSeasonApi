using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using ProduceInSeasonApi.Models;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("PRODUCE_DB", EnvironmentVariableTarget.User);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ProductContext>(opt =>
    opt.UseNpgsql(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();