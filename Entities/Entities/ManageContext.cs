using Entities.SeedData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class ManageContext : IdentityDbContext<AppUser>
    {
        public ManageContext(DbContextOptions<ManageContext> options) : base(options) { }

        public ManageContext() { }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configData = new ConfigData();
            modelBuilder.Entity<Product>().HasOne<Category>(p => p.Category).WithMany(m => m.Products).HasForeignKey(p => p.IDCategory).OnDelete(DeleteBehavior.Cascade);
            configData.ConfigDataAccount(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //refer: https://docs.microsoft.com/en-us/ef/core/querying/related-data#lazy-loading
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                // Get connection string from WebMVC in appsettings.js file
                // package: Microsoft.Extensions.Configuration.Json
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("ManageDb");
                //refer at https://docs.microsoft.com/en-us/ef/core/querying/related-data#include-on-derived-types
                //pacakge: Microsoft.EntityFrameworkCore.SqlServer
                optionsBuilder.UseSqlServer(connectionString).ConfigureWarnings(warning => warning.Throw());

            }
        }
    }
}
