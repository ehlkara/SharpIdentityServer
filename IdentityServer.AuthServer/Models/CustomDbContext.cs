using Microsoft.EntityFrameworkCore;

namespace IdentityServer.AuthServer.Models
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions opts) : base(opts)
        {
        }

        public DbSet<CustomUser> CustomUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // md5 sha256 sha512

            // hashMake hashValid
            modelBuilder.Entity<CustomUser>().HasData(
                new CustomUser()
                {
                    Id = 1,
                    Email = "ehlkara@hotmail.com",
                    Password = "password",
                    City = "Istanbul",
                    UserName = "ehlkara"
                },
            new CustomUser()
            {
                Id = 2,
                Email = "ahmet@hotmail.com",
                Password = "password",
                City = "Sivas",
                UserName = "ahmet16"
            }, new CustomUser()
            {
                Id = 3,
                Email = "mehmet@hotmail.com",
                Password = "password",
                City = "Balıkesir",
                UserName = "mehmet07"
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
