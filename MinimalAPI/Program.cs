// https://www.youtube.com/watch?v=RRrsFE6OXAQ&t=4130s

using Application;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Domain;
using MinimalAPI;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();

foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RegisterEndpointsDefinitions();
app.Run();
