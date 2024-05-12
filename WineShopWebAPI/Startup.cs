//using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;
//using WineShopWebAPI.Authentication;
//using WineShopWebAPI.Data;


//namespace WineShopWebAPI
//{
//    public class Startup
//    {
//        // Other methods and properties

//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            // Other configurations

//            using (var scope = app.ApplicationServices.CreateScope())
//            {
//                var services = scope.ServiceProvider;
//                var context = services.GetRequiredService<ApplicationDbContext>();

//                // Apply pending migrations
//                context.Database.Migrate();

//                // Seed default data
//                SeedData.Initialize(services).Wait();
//            }
//        }
//        //public void ConfigureServices(IServiceCollection services)
//        //{
//        //    // Other service configurations...

//        //    // Add Swagger services
//        //    services.AddSwaggerGen(c =>
//        //    {
//        //        c.SwaggerDoc("v1", new OpenApiInfo
//        //        {
//        //            Title = "Wine Store API documentation",
//        //            Version = "v1"
//        //        });
//        //    });
           
//        //}
//    }
//}

