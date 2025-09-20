using GlobalAuditService.Enums;

namespace GlobalAuditService.Entities
{
    public class AuditEntry
    {
        public int Id { get; set; }
        public string EntityName { get; set; } = null!;
        public AuditAction Action { get; set; }
        public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; } = null!;
        public List<AuditChange> Changes { get; set; } = new();
    }
}
