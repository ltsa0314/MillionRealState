using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MillionRealState.Application.Abstractions.Services;
using MillionRealState.Application.Common.Exceptions;
using MillionRealState.Application.Features.Owner.Dtos;


namespace MillionRealState.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                var owner = await _ownerService.GetByIdAsync(id, ct);
                return Ok(owner);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOwnerDto dto, CancellationToken ct)
        {
            try
            {
                var id = await _ownerService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id }, null);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOwnerDto dto, CancellationToken ct)
        {
            try
            {
                await _ownerService.UpdateAsync(id, dto, ct);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] OwnerFilterDto filter, CancellationToken ct)
        {
            var result = await _ownerService.ListAsync(filter, ct);
            return Ok(result);
        }
    }
}   