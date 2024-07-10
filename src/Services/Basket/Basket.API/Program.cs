using System.ComponentModel;
using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks;
using BuildingBlocks.Exceptions.Handler;
using FluentValidation;
using Marten;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
var builder = WebApplication.CreateBuilder(args);


var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviors<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database"));
builder.Services.AddMarten(config => { config.Connection(builder.Configuration.GetConnectionString("Database")!);

config.Schema.For<ShoppingCart>().Identity(x=>x.UserName);


 });

 builder.Services.AddStackExchangeRedisCache(options=>{
options.Configuration=builder.Configuration.GetConnectionString("Redis");

 });
builder.Services.AddCarter();
builder.Services.AddScoped<IBasketRepository,BasketRepository>();

builder.Services.Decorate<IBasketRepository,CachingBasketRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();
app.MapCarter();
app.UseHealthChecks("/health",new HealthCheckOptions{

    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.Run();
