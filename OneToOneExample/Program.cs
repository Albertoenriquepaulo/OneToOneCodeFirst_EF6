﻿using Microsoft.EntityFrameworkCore;
using OneToOneExample.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OneToOneDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OneToOneDbContext") ?? throw new InvalidOperationException("Connection string 'OneToOneDbContext' not found.")));

// Add services to the container.


//Esto corrige el error “A possible object cycle was detected”
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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
