using Authentication_and_Authorization.Data;
using Microsoft.EntityFrameworkCore;

namespace Authentication_and_Authorization.Extensions
{
    public static class AddMigration
    {
        public static IApplicationBuilder ApplyMigration(this IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }

            return app;
        }
    }
}
