using GlobalAuditService.DTOs;
using GlobalAuditService.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalAuditService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    [Produces("application/json")]
    public class AuditController : ControllerBase
    {
        private readonly IAuditService _service;

        public AuditController(IAuditService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates new audit entry with old and new values.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(AuditResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAuditDetail([FromBody] AuditRequestDto requestDto, CancellationToken ct)
        {
            var result = await _service.CreateAuditAsync(requestDto, ct);
            return CreatedAtAction(nameof(GetAuditDetailsById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Return audit details by passing auditId param.
        /// </summary>
        [HttpGet("{auditId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAuditDetailsById(int auditId, CancellationToken ct)
        {
            var result = await _service.GetByIdAsync(auditId, ct);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
