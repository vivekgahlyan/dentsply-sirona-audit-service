using GlobalAuditService.Enums;

namespace GlobalAuditService.DTOs
{
    public class AuditResponseDto
    {
        public int Id { get; set; }
        public string EntityName { get; set; } = null!;
        public AuditAction Action { get; set; }
        public DateTime TimestampUtc { get; set; }
        public string UserId { get; set; } = null!;
        public List<AuditChangeDto> Changes { get; set; } = new();
    }
}
