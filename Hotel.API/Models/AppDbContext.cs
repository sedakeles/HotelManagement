using Microsoft.EntityFrameworkCore;

namespace Hotel.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<ContactInformations> ContactInformations { get; set; }
    }
}
