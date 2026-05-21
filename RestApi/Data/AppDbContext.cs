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

        // DbSets
        public DbSet<World> Worlds { get; set; }
        public DbSet<WorldSection> WorldSections { get; set; }
        public DbSet<WorldBlock> WorldBlocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ReportWorld> ReportWorlds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // World
            modelBuilder.Entity<World>(entity =>
            {
                entity.ToTable("Worlds");
                entity.HasKey(w => w.Id);
                entity.Property(w => w.Name).IsRequired().HasMaxLength(50);
                entity.Property(w => w.Description).HasMaxLength(500);
                entity.Property(w => w.WorldType).HasConversion<string>();
                entity.Property(w => w.IsPublic).HasDefaultValue(false);

                // 1 World -> Many Sections
                entity.HasMany(w => w.Sections)
                      .WithOne(s => s.World)
                      .HasForeignKey(s => s.WorldId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // WorldSection
            modelBuilder.Entity<WorldSection>(entity =>
            {
                entity.ToTable("WorldSections");
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Title).IsRequired().HasMaxLength(100);

                // 1 Section -> Many Blocks
                entity.HasMany(s => s.Blocks)
                      .WithOne(b => b.Section)
                      .HasForeignKey(b => b.WorldSectionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // WorldBlock
            modelBuilder.Entity<WorldBlock>(entity =>
            {
                entity.ToTable("WorldBlocks");
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Name).HasMaxLength(50);
                entity.Property(b => b.Content).HasMaxLength(1000);
            });
        }
    }
}
