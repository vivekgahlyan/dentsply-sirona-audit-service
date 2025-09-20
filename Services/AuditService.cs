using AutoMapper;
using GlobalAuditService.DTOs;
using GlobalAuditService.Entities;
using GlobalAuditService.Repositories;
using System.Text.Json;

namespace GlobalAuditService.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _repo;
        private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);
        private readonly IMapper _mapper;

        public AuditService(IAuditRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AuditResponseDto> CreateAuditAsync(AuditRequestDto requestDto, CancellationToken ct = default)
        {
            var changes = ComputeChanges(requestDto.Before, requestDto.After)
                .Select(c => new AuditChange
                {
                    PropertyName = c.PropertyName,
                    OldValue = c.OldValue,
                    NewValue = c.NewValue
                }).ToList();

            var entry = new AuditEntry
            {
                EntityName = requestDto.EntityName,
                UserId = requestDto.UserId,
                Action = requestDto.Action,
                TimestampUtc = DateTime.UtcNow,
                Changes = changes
            };

            var saved = await _repo.AddAuditDetailAsync(entry, ct);
            return _mapper.Map<AuditResponseDto>(saved);
        }

        public async Task<AuditResponseDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetAuditDetailByIdAsync(id, ct);
            return entity == null ? null : _mapper.Map<AuditResponseDto>(entity);
        }

        private record Change(string PropertyName, string? OldValue, string? NewValue);

        private List<Change> ComputeChanges(JsonElement? before, JsonElement? after)
        {
            var changes = new List<Change>();
            var beforeDict = JsonElementToDictionary(before);
            var afterDict = JsonElementToDictionary(after);

            var keys = new HashSet<string>(beforeDict.Keys, StringComparer.OrdinalIgnoreCase);
            keys.UnionWith(afterDict.Keys);

            foreach (var key in keys.OrderBy(k => k))
            {
                beforeDict.TryGetValue(key, out var bVal);
                afterDict.TryGetValue(key, out var aVal);

                string? bJson = bVal.HasValue ? JsonSerializer.Serialize(bVal.Value, _jsonOptions) : null;
                string? aJson = aVal.HasValue ? JsonSerializer.Serialize(aVal.Value, _jsonOptions) : null;

                if (bJson != aJson)
                {
                    changes.Add(new Change(key, bJson, aJson));
                }
            }
            return changes;
        }

        private Dictionary<string, JsonElement?> JsonElementToDictionary(JsonElement? elem)
        {
            var dict = new Dictionary<string, JsonElement?>(StringComparer.OrdinalIgnoreCase);

            if (elem is null || elem.Value.ValueKind == JsonValueKind.Null) return dict;

            if (elem.Value.ValueKind != JsonValueKind.Object)
            {
                dict["$"] = elem.Value;
                return dict;
            }

            foreach (var prop in elem.Value.EnumerateObject())
            {
                dict[prop.Name] = prop.Value;
            }
            return dict;
        }
    }
}
