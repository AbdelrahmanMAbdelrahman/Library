using Library.Application.Features.Reservations.Commands.CreateReservations;
using Library.Application.Features.Reservations.Dtos;
using Library.Application.Features.Reservations.Queries.GetReservations;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    [Route("api/[Controller]")]
    public class ReservationsController(ISender sender):ApiController
    {
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [EndpointName(nameof(GetReservation))]
        [EndpointSummary("return Reservation")]
        [EndpointDescription("return Reservation by provide an id")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetReservation([FromRoute]GetReservationQuery query,CancellationToken ct)
        {
            Result<ReservationDto> result = await sender.Send(query,ct);
            return result.Match(res => Ok(res), Problem);
        }
        [HttpPost("")]
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [EndpointName(nameof(CreateReservation))]
        [EndpointSummary("return created Reservation")]
        [EndpointDescription("return Reservation after creation")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> CreateReservation(CreateReservationCommand command,CancellationToken ct)
        {
            Result<ReservationDto>result=await sender.Send(command,ct);
            return result.Match(
                res=>CreatedAtAction(nameof(GetReservation),new { Id=res.Id},res),
                Problem);
        }
        [HttpGet("")]
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [EndpointName(nameof(GetReservations))]
        [EndpointSummary("return Reservations")]
        [EndpointDescription("return Reservations with criteria")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetReservations([FromQuery]GetReservationsQuery query,CancellationToken ct)
        {
            Result<PaginatedList<ReservationDto>> result = await sender.Send(query, ct);
            return result.Match(res=>Ok(res),Problem);
        }
    }
}
