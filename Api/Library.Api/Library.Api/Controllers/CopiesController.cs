using Library.Application.Common.Models;
using Library.Application.Features.Books.Commands.DeleteBooks;
using Library.Application.Features.Books.Commands.UpdateBooks;
using Library.Application.Features.Books.Dtos;
using Library.Application.Features.Books.Queries.GetBooks;
using Library.Application.Features.Books.Queries.GetCopy;


namespace Library.Api.Controllers;
[Route("api/[controller]")]
public class CopiesController(ISender sender):ApiController
{
    [HttpPost()]
    [ProducesResponseType(typeof(CopyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointName(nameof(CreateBook))]
    [EndpointSummary("create borrowing record")]
    [EndpointDescription("return created book")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> CreateBook([FromForm]BookReq book,CancellationToken ct)
    {
        CreateBookCommand createBookCommand = new CreateBookCommand(
            book.Title,book.ISBN,book.Genere,book.AdditionalDetails,book.PublicationDate,
            book.NumberOfCopies,book.Image.OpenReadStream(),book.Image.FileName,book.Image.ContentType
            );
        var result = await sender.Send(createBookCommand);
        return result.Match(
            res => CreatedAtAction(nameof(GetCopy),new{ Id= result.Value.Id},res),
            Problem
            );
    }
    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(CopyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [EndpointName(nameof(GetCopy))]
    [EndpointSummary("return copy")]
    [EndpointDescription("return book by provide an id")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetCopy([FromRoute] GetCopyQuery query,CancellationToken ct)
    {
        var result = await sender.Send(query);
        return result.Match(
            res=>Ok(res),
            Problem
            );
    }
    [HttpGet()]
    [ProducesResponseType(typeof(PaginatedList<CopyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointName(nameof(GetCopies))]
    [EndpointSummary("return copies")]
    [EndpointDescription("return All copies")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetCopies([FromQuery]GetCopiesQuery query,CancellationToken ct)
    {
        var result = await sender.Send(query,ct);
        return result.Match(
            res=>Ok(res), Problem);
    }
    //[HttpGet("CopyDto/{Id}")]
    //[ProducesResponseType(typeof(CopyDto), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    //[EndpointName(nameof(GetCopyDto))]
    //[EndpointSummary("return CopyDto")]
    //[EndpointDescription("return first available CopyDto by book id")]
    //public async Task<IActionResult> GetCopyDto([FromRoute]GetCopyQuery command,CancellationToken ct)
    //{
    //    Result<CopyDto> copiesResult =await sender.Send(command, ct);
    //    return copiesResult.Match(res=> Ok(res),Problem);
    //}
    [HttpPut("{Id}")]
    [ProducesResponseType( StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointName(nameof(UpdateBook))]
    [EndpointSummary("update Copies")]
    [EndpointDescription("update All Copies")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateBook([FromRoute]Guid Id, [FromForm]UpdateBookReq req,CancellationToken ct) {
        UpdateBookCommand command = new UpdateBookCommand(Id,req.Title,req.ISBN,req.Genere,req.AdditionalDetails,
            req.PublicationDate,req.Image?.OpenReadStream(),req.Image?.ContentType,req.Image?.FileName);
        var result = await sender.Send(command,ct);
        return result.Match(res=>NoContent(),Problem);
    }
    [HttpDelete("{Id}")]
    [ProducesResponseType( StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointName(nameof(DeleteCopy))]
    [EndpointSummary("deletes copy")]
    [EndpointDescription("deletes copy by id")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteCopy([FromRoute] DeleteCopyCommand command,CancellationToken ct) {
        var result = await sender.Send(command, ct);
        return result.Match(res=>NoContent(),Problem);
    }
}
