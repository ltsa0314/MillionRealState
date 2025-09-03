        using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MillionRealState.Application.Abstractions.Services;
using MillionRealState.Application.Common.Exceptions;
using MillionRealState.Application.Features.Owner.Dtos;

namespace MillionRealState.API.Controllers
{
    /// <summary>
    /// API controller for managing property owners.
    /// Provides endpoints to create, update, retrieve and list owners.
    /// </summary>
    [ApiController] 
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerController"/>.
        /// </summary>
        /// <param name="ownerService">Service for owner operations.</param>
        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        /// <summary>
        /// Gets an owner by their unique identifier.
        /// </summary>
        /// <param name="id">Owner's unique identifier.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Returns the owner data if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        [ActionName("GetOwnerById")]
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

        /// <summary>
        /// Creates a new owner.
        /// </summary>
        /// <param name="dto">Data for the new owner.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Returns the location of the created owner.</returns>
        [HttpPost]
        [ActionName("CreateOwner")]
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

        /// <summary>
        /// Updates an existing owner.
        /// </summary>
        /// <param name="id">Owner's unique identifier.</param>
        /// <param name="dto">Updated owner data.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>No content if successful; otherwise, error details.</returns>
        [HttpPut("{id}")]
        [ActionName("UpdateOwner")]
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

        /// <summary>
        /// Lists owners with optional filters and pagination.
        /// </summary>
        /// <param name="filter">Filter and pagination options.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Paged list of owners.</returns>
        [HttpGet]
        [ActionName("ListOwners")]
        public async Task<IActionResult> List([FromQuery] OwnerFilterDto filter, CancellationToken ct)
        {
            var result = await _ownerService.ListAsync(filter, ct);
            return Ok(result);
        }
    }
}