using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MillionRealState.Application.Common.Exceptions;
using MillionRealState.Application.Features.Owner.Commands;
using MillionRealState.Application.Features.Owner.Dtos;
using MillionRealState.Application.Features.Owner.Queries;

namespace MillionRealState.API.Controllers
{
    /// <summary>
    /// API controller for managing property owners.
    /// Provides endpoints to create, update, retrieve and list owners.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerController"/>.
        /// </summary>
        /// <param name="mediator">MediatR instance for sending commands and queries.</param>
        public OwnerController(IMediator mediator)
        {
            _mediator = mediator;
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
                var owner = await _mediator.Send(new GetOwnerByIdQuery(id), ct);
                if (owner == null)
                    return NotFound(new { error = "No se encontró el propietario." });
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
                var id = await _mediator.Send(new CreateOwnerCommand(dto), ct);
                return CreatedAtAction("GetOwnerById", new { id = id }, null);
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
                await _mediator.Send(new UpdateOwnerCommand(id, dto), ct);
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
            var result = await _mediator.Send(new ListOwnersQuery(filter), ct);
            return Ok(result);
        }
    }
}