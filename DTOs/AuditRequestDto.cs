using GlobalAuditService.Enums;
using System.Text.Json;

namespace GlobalAuditService.DTOs
{
    public class AuditRequestDto
    {
        public string EntityName { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public AuditAction Action { get; set; }
        public JsonElement? Before { get; set; }
        public JsonElement? After { get; set; }
    }
}
