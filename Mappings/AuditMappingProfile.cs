using AutoMapper;
using GlobalAuditService.DTOs;
using GlobalAuditService.Entities;

namespace GlobalAuditService.Mappings
{
    public class AuditMappingProfile : Profile
    {
        public AuditMappingProfile()
        {
            CreateMap<AuditEntry, AuditResponseDto>().ReverseMap();
            CreateMap<AuditChange, AuditChangeDto>().ReverseMap();
        }
    }
}
