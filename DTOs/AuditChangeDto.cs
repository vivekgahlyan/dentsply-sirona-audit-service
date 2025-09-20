namespace GlobalAuditService.DTOs
{
    public class AuditChangeDto
    {
        public string PropertyName { get; set; } = null!;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
    }
}
