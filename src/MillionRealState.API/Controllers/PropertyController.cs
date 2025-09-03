using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MillionRealState.Application.Abstractions.Services;
using MillionRealState.Application.Common.Exceptions;
using MillionRealState.Application.Features.Properties.Dtos;

namespace MillionRealState.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                var property = await _propertyService.GetByIdAsync(id, ct);
                return Ok(property);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePropertyDto dto, CancellationToken ct)
        {
            try
            {
                var id = await _propertyService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id }, null);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePropertyDto dto, CancellationToken ct)
        {
            try
            {
                await _propertyService.UpdateAsync(id, dto, ct);
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
        public async Task<IActionResult> List([FromQuery] PropertyFilterDto filter, CancellationToken ct)
        {
            var result = await _propertyService.ListAsync(filter, ct);
            return Ok(result);
        }

        // Ejemplo para agregar imagen
        [HttpPost("{id}/image")]
        public async Task<IActionResult> AddImage(Guid id, [FromBody] AddPropertyImageDto dto, CancellationToken ct)
        {
            try
            {
                await _propertyService.AddImageAsync(id, dto, ct);
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

        // Ejemplo para cambiar precio
        [HttpPut("{id}/price")]
        public async Task<IActionResult> ChangePrice(Guid id, [FromBody] ChangePriceDto dto, CancellationToken ct)
        {
            try
            {
                await _propertyService.ChangePriceAsync(id, dto, ct);
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
    }
}   