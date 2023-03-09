using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TadWhat.Domain;

namespace TadWhat.Repository
{
    public class DatabaseContext : IdentityDbContext<IdentityUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasData(
                new User
                {
                    Id = new Guid("b0d4ce5d-2757-4699-948c-cfa72ba94f86"),
                    Name = "NameDefault",
                    Email = "EmailDefault",
                    Password = "PasswordDefault"
                });
        }

        public DbSet<User> user { get; set; }
    }
}
