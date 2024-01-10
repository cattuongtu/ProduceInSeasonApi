using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using ProduceInSeasonApi.Models;
using System;
using System.IO;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("*");
        });
});

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

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();