using Library.Application.Features.Fines.Commands.PayFines;
using Library.Application.Features.Fines.Queries.GetFineById;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    public class FineController(ISender sender):ApiController
    {
        [HttpGet()]
        [EndpointName(nameof( GetFines))]
        [EndpointSummary("get fines")]
        [EndpointDescription("return fines with cretieria")]
        [ProducesResponseType(typeof(PaginatedList<FineDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]
        [Authorize("User")]
        public async Task<IActionResult> GetFines([FromQuery]GetFinesQuery query,CancellationToken ct)
        {
            Result<PaginatedList<FineDto>> GetFineResult = await sender.Send(query);
            return GetFineResult.Match(res => Ok(res), Problem);
        }
        [HttpGet("{Id}")]
        [EndpointName(nameof( GetFine))]
        [EndpointSummary("get fine")]
        [EndpointDescription("return fine with Id")]
        [ProducesResponseType(typeof(PaginatedList<FineDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]
        [Authorize("User")]
        public async Task<IActionResult> GetFine([FromRoute]GetFineQuery query,CancellationToken ct)
        {
            Result<FineDto> GetFineResult = await sender.Send(query);
            return GetFineResult.Match(res => Ok(res), Problem);
        }
        [HttpPut("{Id}")]
        [EndpointName(nameof(PayFine))]
        [EndpointSummary("Pay fine")]
        [EndpointDescription("Pay fine with Id")]
        [ProducesResponseType(typeof(PaginatedList<FineDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Authorize("User")]
        public async Task<IActionResult> PayFine([FromRoute]PayFineCommand command,CancellationToken ct)
        {
            Result<Updated> result = await sender.Send(command,ct);
            return result.Match(res => NoContent(), Problem);
        }
    }
}
