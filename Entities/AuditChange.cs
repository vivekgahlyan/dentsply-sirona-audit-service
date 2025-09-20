namespace GlobalAuditService.Entities
{
    public class AuditChange
    {
        public int Id { get; set; }
        public int AuditEntryId { get; set; }
        public string PropertyName { get; set; } = null!;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
    }
}
