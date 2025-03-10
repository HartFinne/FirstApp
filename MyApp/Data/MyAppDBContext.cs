using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Data
{
    public class MyAppDBContext : DbContext
    {
        public MyAppDBContext(DbContextOptions<MyAppDBContext> options) :base(options) {}

        public DbSet<Item> Items { get; set; }
    }
    // OEDIPUSJUDE\SQLEXPRESS
}
