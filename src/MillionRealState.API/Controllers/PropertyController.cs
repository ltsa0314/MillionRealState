using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MillionRealState.Application.Abstractions.Services;
using MillionRealState.Application.Common.Exceptions;
using MillionRealState.Application.Features.Properties.Dtos;

namespace MillionRealState.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyController"/>.
        /// </summary>
        /// <param name="propertyService">Service for property operations.</param>
        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        /// <summary>
        /// Gets a property by its unique identifier.
        /// </summary>
        /// <param name="id">Property unique identifier.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Returns the property data if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        [ActionName("GetPropertyById")]
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

        /// <summary>
        /// Creates a new property.
        /// </summary>
        /// <param name="dto">Data for the new property.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Returns the location of the created property.</returns>
        [HttpPost]
        [ActionName("CreateProperty")]
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

        /// <summary>
        /// Updates an existing property.
        /// </summary>
        /// <param name="id">Property unique identifier.</param>
        /// <param name="dto">Updated property data.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>No content if successful; otherwise, error details.</returns>
        [HttpPut("{id}")]
        [ActionName("UpdateProperty")]
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

        /// <summary>
        /// Lists properties with optional filters and pagination.
        /// </summary>
        /// <param name="filter">Filter and pagination options.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Paged list of properties.</returns>
        [HttpGet]
        [ActionName("ListProperties")]
        public async Task<IActionResult> List([FromQuery] PropertyFilterDto filter, CancellationToken ct)
        {
            var result = await _propertyService.ListAsync(filter, ct);
            return Ok(result);
        }
        /// <summary>
        /// Adds an image to a property.
        /// </summary>
        /// <param name="id">Property unique identifier.</param>
        /// <param name="dto">Image data to add.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>No content if successful; otherwise, error details.</returns>
        [HttpPost("{id}/image")]
        [ActionName("AddImageToProperty")]
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


        /// <summary>
        /// Changes the price of a property.
        /// </summary>
        /// <param name="id">Property unique identifier.</param>
        /// <param name="dto">New price data.</param>
        /// <param name="ct">Cancellation token.</param>
        [HttpPut("{id}/price")]
        [ActionName("ChangePropertyPrice")]
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