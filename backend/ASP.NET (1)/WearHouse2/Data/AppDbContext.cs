using Microsoft.EntityFrameworkCore;
using WearHouse.Models;

namespace WearHouse.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> MsUser { get; set; }
        public DbSet<Category> MsCategory { get; set; }
    }
}
