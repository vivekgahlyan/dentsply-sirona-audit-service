using GlobalAuditService.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlobalAuditService.Data
{
    public class AuditDbContext : DbContext
    {
        public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options) { }

        public DbSet<AuditEntry> AuditEntries { get; set; } = null!;
        public DbSet<AuditChange> AuditChanges { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditEntry>()
                .HasMany(a => a.Changes)
                .WithOne()
                .HasForeignKey(c => c.AuditEntryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
