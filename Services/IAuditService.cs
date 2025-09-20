using GlobalAuditService.DTOs;

namespace GlobalAuditService.Services
{
    public interface IAuditService
    {
        Task<AuditResponseDto> CreateAuditAsync(AuditRequestDto dto, CancellationToken ct = default);
        Task<AuditResponseDto?> GetByIdAsync(int id, CancellationToken ct = default);
    }
}
