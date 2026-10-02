using Library.Application.Common.Models;
using Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecords;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    [Route("api/[Controller]")]
    //[Authorize]
    public class BorrowingRecordsController(ISender sender):ApiController
    {
        [HttpPost()]
        [ProducesResponseType(typeof(BorrowingRecordDto),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]
        [EndpointName(nameof(CreateBorrowingRecord))]
        [EndpointSummary("pass borrowing record data to create one")]
        [EndpointDescription("provide borrowing record information to create one")]
        [Authorize(Roles ="User,Admin")]
        public async Task<IActionResult> CreateBorrowingRecord(CreateBorrowingRecordCommand command,CancellationToken ct)
        {
            Result<BorrowingRecordDto> result =await sender.Send(command);
            return result.Match(
                response => CreatedAtAction(nameof(GetBorrowingRecord),new {Id=response.Id },response),
                Problem
                );
        }
        [HttpGet("{Id}")]
        [EnableRateLimiting("SlidingWindow")]
        [ProducesResponseType(typeof(BorrowingRecordDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [EndpointName(nameof(GetBorrowingRecord))]
        [EndpointSummary("return borrowing record")]
        [EndpointDescription("return borrowing record by provide an id")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetBorrowingRecord([FromRoute] GetBorrowingRecordQuery command,CancellationToken ct)
        {
            Result<BorrowingRecordDto> result = await sender.Send(command);
            return result.Match(
                response=>Ok(response),
                Problem
                );
        }
        [HttpGet("")]
        [EnableRateLimiting("SlidingWindow")]
        [ProducesResponseType(typeof(PaginatedList< BorrowingRecordDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [EndpointName(nameof(GetBorrowingRecords))]
        [EndpointSummary("return borrowing records")]
        [EndpointDescription("return borrowing records ")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetBorrowingRecords([FromQuery]GetBorrowingRecordsQuery command,CancellationToken ct)
        {
            Result<PaginatedList<BorrowingRecordDto>> paginatedResult = await sender.Send(command,ct);
            return paginatedResult.Match(res=>Ok(res),Problem);
        }
       
    }
}
