using Discout.Grpc.Data;
using Microsoft.EntityFrameworkCore;

namespace Discout.Grpc;

public static class Extensions
{
        public static IApplicationBuilder UseMigrate(this IApplicationBuilder app)
        {
          
             using var scope = app.ApplicationServices.CreateScope();
             using var context = scope.ServiceProvider.GetRequiredService<DiscountConext>();
             context.Database.MigrateAsync();
            return app;
        }

}
