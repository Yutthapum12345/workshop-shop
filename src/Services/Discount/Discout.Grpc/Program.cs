
using Discout.Grpc;
using Discout.Grpc.Data;
using Discout.Grpc.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();


builder.Services.AddDbContext<DiscountConext>(options=>{

    options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});

var app = builder.Build();
app.UseMigrate();
// Configure the HTTP request pipeline.
app.MapGrpcService<DiscoutService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
