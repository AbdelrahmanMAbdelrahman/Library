namespace Library.Api.Controllers
{
    [Route("api/[Controller]")]
    [Authorize]
    public class BorrowingRecordController(ISender sender):ApiController
    {
        [HttpPost(nameof(CreateBorrowingRecord))]
        [ProducesResponseType(typeof(BorrowingRecordDto),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]
        [EndpointName(nameof(CreateBorrowingRecord))]
        [EndpointSummary("pass borrowing record data to create one")]
        [EndpointDescription("provide borrowing record information to create one")]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> CreateBorrowingRecord(CreateBorrowingRecordCommand command,CancellationToken ct)
        {
            Result<BorrowingRecordDto> result =await sender.Send(command);
            return result.Match(
                response => CreatedAtAction(nameof(GetBorrowingRecord),new {Id=response.Id },response),
                Problem
                );
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(BorrowingRecordDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [EndpointName(nameof(GetBorrowingRecord))]
        [EndpointSummary("return borrowing record")]
        [EndpointDescription("return borrowing record by provide an id")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetBorrowingRecord([FromRoute] GetBorrowingRecordCommand command,CancellationToken ct)
        {
            Result<BorrowingRecordDto> result = await sender.Send(command);
            return result.Match(
                response=>Ok(response),
                Problem
                );
        }
    }
}
