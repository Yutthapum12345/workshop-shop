
using Marten;
using FluentValidation;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCarter();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(typeof(Program).Assembly);

});
var Assembly = typeof(Program).Assembly;

builder.Services.AddMarten(config =>
{
   
    config.Connection(builder.Configuration.GetConnectionString("Database")!);

}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(Assembly);


var app = builder.Build();


if (app.Environment.IsDevelopment())
{

}

app.MapCarter();
app.UseHttpsRedirection();


app.Run();

