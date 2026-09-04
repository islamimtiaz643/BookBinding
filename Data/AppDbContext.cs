using BookBinding.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BookBinding.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Paper> Papers { get; set; }
        public DbSet<Leather> Leathers { get; set; }
        public DbSet<BindingStyle> BindingStyles { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<JournalImage> JournalImages { get; set; }
        public DbSet<CustomJournalRequest> CustomJournalRequests { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<CustomJournalRequest>()
                .HasOne(r => r.Paper)
                .WithMany()
                .HasForeignKey(r => r.PaperId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}