using Library.Application.Features.Books.Mapper;
using Library.Domain.Books;
using Library.Tests.Common;
using Library.Tests.Common.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Tests.BookMappers
{
    public class BookMapperTests
    {
        [Fact]
        public void BookToDto_ShouldMapCorrectly()
        {
            FakeTimeProvider timeProvider = new FakeTimeProvider();
            string title = "C++";
            string genere = "prog";
            string isbn = "A123";
            string additionalDetails = "no";
            int numberOfCopies = 5;
            var date = timeProvider.GetUtcNow().LocalDateTime;
            Book book = BookFactory.CreateBook(title,date,genere,isbn,null,additionalDetails,numberOfCopies).Value;
            var dto = book.ToDto();
            Assert.NotNull(dto);
            Assert.Equal(title,dto.Title);
            Assert.Equal(genere, dto.Genere);
            Assert.Equal(isbn,dto.ISBN);
            Assert.Equal(additionalDetails,dto.AdditionalDetails);
            
        }
    }                           
}
