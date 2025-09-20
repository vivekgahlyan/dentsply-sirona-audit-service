using GlobalAuditService.Data;
using GlobalAuditService.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlobalAuditService.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AuditDbContext _dbContext;

        public AuditRepository(AuditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuditEntry> AddAuditDetailAsync(AuditEntry newEntry, CancellationToken ct = default)
        {
            _dbContext.AuditEntries.Add(newEntry);
            await _dbContext.SaveChangesAsync(ct);
            return newEntry;
        }

        public async Task<AuditEntry?> GetAuditDetailByIdAsync(int id, CancellationToken ct = default)
        {
            return await _dbContext.AuditEntries
                .Include(a => a.Changes)
                .FirstOrDefaultAsync(a => a.Id == id, ct);
        }
    }
}
