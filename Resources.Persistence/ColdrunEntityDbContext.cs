using Microsoft.EntityFrameworkCore;
using Resources.Domain.Entities;

namespace Resources.Persistence
{
    public class ColdrunEntityDbContext : DbContext
    {
        public virtual DbSet<Truck> Trucks { get; set; }
        public ColdrunEntityDbContext(DbContextOptions<ColdrunEntityDbContext> options) : base(options)
        {
            Database.EnsureCreatedAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
