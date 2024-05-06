using Microsoft.EntityFrameworkCore;
using WineShopWebAPI.Authentication;
using WineShopWebAPI.Data;


namespace WineShopWebAPI
{
    public class Startup
    {
        // Other methods and properties

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Other configurations

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();

                // Apply pending migrations
                context.Database.Migrate();

                // Seed default data
                SeedData.Initialize(services).Wait();
            }
        }
    }
}

