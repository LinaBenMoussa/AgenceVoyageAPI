using AgenceVoyage.Models;
using Microsoft.EntityFrameworkCore;
using System;
using AgenceVoyage.Controllers;

var builder = WebApplication.CreateBuilder(args);
var connectionstring = builder.Configuration
    .GetConnectionString("Connection");
builder.Services.AddDbContext<ClientDbContext>(options =>
options.UseSqlServer(connectionstring));

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
