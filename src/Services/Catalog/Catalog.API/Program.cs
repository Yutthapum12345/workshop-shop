

using BuildingBlocks;
using BuildingBlocks.Exceptions.Handler;
using Catalog.API.Data;
using FluentValidation;
using HealthChecks.UI.Client;
using HealthChecks.UI.Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database"));
builder.Services.AddMarten(config =>
{
   
    config.Connection(builder.Configuration.GetConnectionString("Database")!);

}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(Assembly);

if(builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}


builder.Services.AddExceptionHandler<CustomExceptionHandler>();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{

}


app.MapCarter();
app.UseHttpsRedirection();

app.UseExceptionHandler(options=>{

});

app.UseHealthChecks("/health",new HealthCheckOptions{

    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

