using Library.Application.Common.Models;
using Library.Application.Features.Books.Commands.DeleteBooks;
using Library.Application.Features.Books.Commands.UpdateBooks;
using Library.Application.Features.Books.Dtos;
using Library.Application.Features.Books.Queries.GetBooks;
using System.Threading.Tasks;

namespace Library.Api.Controllers;
[Route("api/[controller]")]
public class BookController(ISender sender):ApiController
{
    [HttpPost()]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointName(nameof(CreateBook))]
    [EndpointSummary("create borrowing record")]
    [EndpointDescription("return created book")]
    //[Authorize(Roles = "User")]
    public async Task<IActionResult> CreateBook([FromForm]BookReq book,CancellationToken ct)
    {
        CreateBookCommand createBookCommand = new CreateBookCommand(
            book.Title,book.ISBN,book.Genere,book.AdditionalDetails,book.PublicationDate,
            book.NumberOfCopies,book.Image.OpenReadStream(),book.Image.FileName,book.Image.ContentType
            );
        var result = await sender.Send(createBookCommand);
        return result.Match(
            res => CreatedAtAction(nameof(GetBook),new{ Id= result.Value.Id },res),
            Problem
            );
    }
    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [EndpointName(nameof(GetBook))]
    [EndpointSummary("return book")]
    [EndpointDescription("return book by provide an id")]
    //[Authorize(Roles = "User")]
    public async Task<IActionResult> GetBook([FromRoute] GetBookQuery query,CancellationToken ct)
    {
        var result = await sender.Send(query);
        return result.Match(
            res=>Ok(res),
            Problem
            );
    }
    [HttpGet()]
    [ProducesResponseType(typeof(PaginatedList<BookDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointName(nameof(GetBooks))]
    [EndpointSummary("return books")]
    [EndpointDescription("return All Books")]
    //[Authorize(Roles = "User")]
    public async Task<IActionResult> GetBooks([FromQuery]GetBooksQuery query,CancellationToken ct)
    {
        var result = await sender.Send(query,ct);
        return result.Match(
            res=>Ok(res), Problem);
    }
    [HttpPut("{Id}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointName(nameof(UpdateBook))]
    [EndpointSummary("return books")]
    [EndpointDescription("return All Books")]
    public async Task<IActionResult> UpdateBook([FromRoute]Guid Id, [FromForm]UpdateBookReq req,CancellationToken ct) {
        UpdateBookCommand command = new UpdateBookCommand(Id,req.Title,req.ISBN,req.Genere,req.AdditionalDetails,
            req.PublicationDate,req.Image.OpenReadStream(),req.Image.ContentType,req.Image.FileName);
        var result = await sender.Send(command,ct);
        return result.Match(res=>NoContent(),Problem);
    }
    [HttpDelete("{Id}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointName(nameof(DeleteBook))]
    [EndpointSummary("updates book")]
    [EndpointDescription("updates Book by id")]
    public async Task<IActionResult> DeleteBook([FromRoute] DeleteBookCommand command,CancellationToken ct) {
        var result = await sender.Send(command, ct);
        return result.Match(res=>NoContent(),Problem);
    }
}
