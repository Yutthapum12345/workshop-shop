
using Marten;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCarter();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(typeof(Program).Assembly);

});


builder.Services.AddMarten(config =>
{
   
    config.Connection(builder.Configuration.GetConnectionString("Database")!);

});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{

}

app.MapCarter();
app.UseHttpsRedirection();


app.Run();

