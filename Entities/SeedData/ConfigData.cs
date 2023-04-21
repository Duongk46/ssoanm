using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Entities.SeedData
{
    public class ConfigData
    {
        public async void ConfigDataAccount(ModelBuilder modelBuilder)
        {
            var passwordHash = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(
               new AppUser
               {
                   Id = System.Guid.NewGuid().ToString(),
                   UserName = "admin",
                   Address = "123 Phạm Văn Đồng",
                   PasswordHash = passwordHash.HashPassword(null, "Admin@123"),
                   Name = "Dương",
                   NormalizedUserName = "ADMIN",
                   Gender = 1,
                   BirthDay = DateTime.Now,
                   Email = "minhduong18072002@gmail.com",
                   NormalizedEmail = "MINHDUONG18072002@GMAIL.COM",
                   EmailConfirmed = true,
               }
            );
        }
    }
}
