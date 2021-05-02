using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}