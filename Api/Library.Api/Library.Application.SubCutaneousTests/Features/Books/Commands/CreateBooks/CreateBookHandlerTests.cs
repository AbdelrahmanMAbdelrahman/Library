using Library.Application.Common.Interfaces;
using Library.Application.Features.Books.Commands.CreateBooks;
using Library.Application.SubCutaneousTests.Common;
using Library.Domain.Books;
using Library.Tests.Common.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;

namespace Library.Application.SubCutaneousTests.Features.Books.Commands.CreateBooks
{
    [Collection(WebAppFactoryCollection.CollectionName)]
    public class CreateBookHandlerTests(WebAppFactory factory)
    {
        private readonly IMediator mediator = factory.CreateMediator();
        private readonly IAppDbContext context=factory.CreateDbContext();
        [Fact]
        public async Task Handle_ShouldPass_ForValidData()
        {
            Book book = BookFactory.CreateBook().Value;
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync(CancellationToken.None);
            CreateBookCommand command = new CreateBookCommand(book.Title,book.ISBN,book.Genere,book.AdditionalNotes,
                book.PublicationDate,2,null,null,null);
            var result =await mediator.Send(command);
            Assert.True(result.IsSuccess);
            Book? dbBook =await context.Books.FindAsync(book.Id);
            Assert.NotNull(dbBook);
        }
       
    }
}
