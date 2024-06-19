

using BuildingBlocks;
using BuildingBlocks.Exceptions.Handler;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCarter();
var Assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(Assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviors<,>));

});


builder.Services.AddMarten(config =>
{
   
    config.Connection(builder.Configuration.GetConnectionString("Database")!);

}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(Assembly);


builder.Services.AddExceptionHandler<CustomExceptionHandler>();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{

}


app.MapCarter();
app.UseHttpsRedirection();

app.UseExceptionHandler(options=>{

});

app.Run();

