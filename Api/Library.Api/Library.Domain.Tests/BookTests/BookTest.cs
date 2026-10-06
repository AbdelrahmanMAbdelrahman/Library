using Library.Domain.Books;
using Library.Domain.Common.Results;
using Library.Tests.Common;
using Library.Tests.Common.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Tests.BookTests
{
    public class BookTest
    {
        //[Fact]
        [Theory]
        [InlineData("C++","programming","isbn", null, "",2)]
        public void CreateBook_ShouldSucceed_WithValidData(
            string title, string genere, 
            string isbn, Guid ? fileId, string additionalNotes, int numberOfCopies)
        {
            FakeTimeProvider timeProvider = new FakeTimeProvider();
            var date = timeProvider.GetUtcNow().UtcDateTime;
            Result<Book> bookRes = BookFactory.CreateBook(
                title,date,genere,isbn,fileId,additionalNotes,numberOfCopies);
            Assert.True(bookRes.IsSuccess);
            Book book = bookRes.Value;
            Assert.NotEmpty(title);
            Assert.NotEmpty(genere);
            Assert.NotEmpty(isbn);
            Assert.NotNull(book);
            Assert.Equal(book.Title, title);
            Assert.Equal(book.ISBN, isbn);
            Assert.Equal(book.PublicationDate,date);
            Assert.Equal(book.Genere, genere);
            Assert.Equal(book.AdditionalNotes, additionalNotes);
            
        }
        [Theory]
        [InlineData("C++","programming","isbn", "b83af488-d33f-4b4d-92e2-197a0c8ff80d", "",2)]
        public void CreateBook_ShouldSucceed_WithFileAndValidData(
            string title, string genere, 
            string isbn, string? fileId, string additionalNotes, int numberOfCopies)
        {
            FakeTimeProvider timeProvider = new FakeTimeProvider();
            var date = timeProvider.GetUtcNow().UtcDateTime;
            Result<Book> bookRes = BookFactory.CreateBook(
                title,date,genere,isbn,Guid.Parse(fileId),additionalNotes,numberOfCopies);
            Assert.True(bookRes.IsSuccess);
            Book book = bookRes.Value;
            Assert.NotEmpty(title);
            Assert.NotEmpty(genere);
            Assert.NotEmpty(isbn);
            Assert.NotNull(book);
            Assert.Equal(book.Title, title);
            Assert.Equal(book.ISBN, isbn);
            Assert.Equal(book.PublicationDate,date);
            Assert.Equal(book.Genere, genere);
            Assert.Equal(book.UploadedFileId,Guid.Parse( fileId));
            Assert.Equal(book.AdditionalNotes, additionalNotes);
            
        }
        [Theory]
        [InlineData("", "programming", "isbn", null, "", 2)]
        [InlineData("C++", "", "isbn", null, "", 2)]
        [InlineData("C++", "programming", "", null, "", 2)]
        [InlineData("C++", "programming", "isbn", null, "notes", 0)]
        public void CreateBook_ShouldFail_WithInValidData(
            string title, string genere,
            string isbn, Guid? fileId, string additionalNotes, int numberOfCopies)
        {
            FakeTimeProvider timeProvider = new FakeTimeProvider();
            var date = timeProvider.GetUtcNow().UtcDateTime;
            Result<Book> bookRes = BookFactory.CreateBook(
                title, date, genere, isbn, fileId, additionalNotes, numberOfCopies);
            
            Assert.False(bookRes.IsSuccess);

        }

    }
}
