
using Microsoft.AspNetCore.Identity;
using WineShopWebAPI.Authentication;
using WineShopWebAPI.Models;

namespace WineShopWebAPI.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();


            // Seed roles
  
            foreach (var role in UserRoles.roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }


            // Seed shops
            if (!context.Shops.Any())
            {
                var shop1 = new Shop { Shop_Name = "Shop 1", Location = "Location 1" };
                var shop2 = new Shop { Shop_Name = "Shop 2", Location = "Location 2" };
                var shop3 = new Shop { Shop_Name = "Shop 3", Location = "Location 3" };

                context.Shops.AddRange(shop1, shop2, shop3);
                await context.SaveChangesAsync();
            }


            // Seed users
            if (!context.Users.Any())
            {
                await CreateUser(userManager, "admin@example.com", "Admin123!", UserRoles.Admin, 1, "Admin", "User", "1234567890");
                await CreateUser(userManager, "manager@example.com", "Manager123!", UserRoles.StoreManager, 1, "Sales", "StoreManager", "0987654321"); // Example shop ID
                await CreateUser(userManager, "employee@example.com", "Employee123!", UserRoles.Employee, 1, "Employee", "User", "1122334455"); // Example shop ID


            }

           



            // Seed products
            if (!context.Products.Any())
            {
                var supplier1 = new Supplier { Supplier_Name = "Supplier 1", Contact_Details = "Contact 1" };
                var supplier2 = new Supplier { Supplier_Name = "Supplier 2", Contact_Details = "Contact 2" };

                var product1 = new Product { Product_Name = "Product 1", Description = "Description 1", Price = 10.00m, Supplier = supplier1 };
                var product2 = new Product { Product_Name = "Product 2", Description = "Description 2", Price = 20.00m, Supplier = supplier2 };
                var product3 = new Product { Product_Name = "Product 3", Description = "Description 3", Price = 30.00m, Supplier = supplier1 };

                context.Products.AddRange(product1, product2, product3);
                await context.SaveChangesAsync();
            }
        }

        private static async Task CreateUser(UserManager<User> userManager, string email, string password, string role, int? shopId, string firstName, string lastName, string phoneNumber)
        {
            var user = new User
            {
                UserName = email,
                Email = email,
                Shop_ID = shopId,
                Name = firstName,
                PhoneNumber = phoneNumber,
                Role=role
                
            };
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, role);
        }



    }



}



