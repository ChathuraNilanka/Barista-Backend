using BaristaAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BaristaAPI.Data
{
    public class APIDbContext: DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> dbContextOptions) : base(dbContextOptions) {}

        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Cafe>().HasKey(c => c.Id);
            modelBuilder.Entity<Cafe>()
                        .HasMany(c => c.Employees)
                        .WithOne(e => e.Cafe)
                        .HasForeignKey(e => e.CafeId);
        }
    }
}
