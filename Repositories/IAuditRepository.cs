using GlobalAuditService.Entities;

namespace GlobalAuditService.Repositories
{
    public interface IAuditRepository
    {
        Task<AuditEntry> AddAuditDetailAsync(AuditEntry entry, CancellationToken ct = default);
        Task<AuditEntry?> GetAuditDetailByIdAsync(int id, CancellationToken ct = default);
    }
}
