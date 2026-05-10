using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestApi.Models;

namespace RestApi.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<World> Worlds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<World>(entity =>
            {
                entity.ToTable("Worlds");
                entity.HasKey(w => w.Id);
                entity.Property(w => w.Name).IsRequired().HasMaxLength(50);
                entity.Property(w => w.Description).HasMaxLength(500);
                entity.Property(w => w.WorldType).HasConversion<string>();
                entity.Property(w => w.IsPublic).HasDefaultValue(false);
            });
        }
    }
}
