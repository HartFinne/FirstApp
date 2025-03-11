using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Data
{
    public class MyAppDBContext : DbContext
    {
        public MyAppDBContext(DbContextOptions<MyAppDBContext> options) :base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Niggas", Price = 40, SerialNumberId = 1 }
                );
            modelBuilder.Entity<SerialNumber>().HasData(
                new SerialNumber { Id = 1, Name = "NIGS150", ItemId = 1 }
                );
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<SerialNumber> SerialNumbers { get; set; }
    }
    // OEDIPUSJUDE\SQLEXPRESS
}
