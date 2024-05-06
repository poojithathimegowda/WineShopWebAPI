using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WineShopWebAPI.Authentication;
using WineShopWebAPI.Models;

namespace WineShopWebAPI.Authentication
{


    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add DbSet properties for your entities
 
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Shop and User entities
            modelBuilder.Entity<Shop>()
                .HasMany(s => s.Employees)   // Shop has many Employees
                .WithOne(u => u.Shop)        // User has one Shop
                .HasForeignKey(u => u.Shop_ID); // Foreign key

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:poojithahome.database.windows.net,1433;Initial Catalog=PoojithaHome;Persist Security Info=False;User ID=poojithaadmin;Password=Ranjith@7591;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}



