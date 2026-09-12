namespace Library.Api.Controllers
{
    [Route("api/[Controller]")]
    public class FileController(ISender sender):ApiController
    {
        [HttpGet("{Id}")]
       
        [ProducesResponseType(typeof(BorrowingRecordDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [EndpointName(nameof(DownloadFile))]
        [EndpointSummary("return image file")]
        [EndpointDescription("return image file by providing an id")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DownloadFile([FromRoute]GetFileQuery query,CancellationToken ct)
        {
            Result<Application.Features.UploadedFiles.Dtos.DownloadFileDto> result = await sender.Send(query,ct);
            return result.Match(res=>File(res.FileStream,res.ContentType,res.StoredFileName),Problem);
        }
    }
}
