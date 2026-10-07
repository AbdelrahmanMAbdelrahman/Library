
using Library.Domain.Books;
using Library.Domain.Copies;
using Library.Domain.Reservations;
using Library.Tests.Common.AppUsers;
using Library.Tests.Common.Books;
using Library.Tests.Common.Reservations;

namespace Library.Application.Tests.Mappers.ReservationMappers;

public class ReservationMapperTests
{
    [Fact]
    public void ToDto_ShouldPass_ForValidData()
    {
        Reservation reservation = ReservationFactory.Create().Value;
        reservation.User = AppUserFactory.Create().Value;
        Book book=BookFactory.CreateBook().Value;
        reservation.Copy = new Domain.Copies.Copy(Guid.NewGuid(),book.Id,Domain.Copies.Enum.CopyStatus.Available);
        reservation.Copy.Book = book;
        Assert.NotNull(reservation);
        Assert.NotNull(reservation.Copy);
        Assert.NotNull(reservation.User);
        Assert.NotNull(reservation.Copy.Book);
    }
    [Fact]
    public void ToDtoList_ShouldPass_ForValidData()
    {
        List< Reservation>reservations = [ReservationFactory.Create().Value];
        var reservation = reservations[0];
        reservation.User = AppUserFactory.Create().Value;
        Book book=BookFactory.CreateBook().Value;
        reservation.Copy = new Domain.Copies.Copy(Guid.NewGuid(),book.Id,Domain.Copies.Enum.CopyStatus.Available);
        reservation.Copy.Book = book;
        Assert.NotNull(reservation);
        Assert.NotNull(reservation.Copy);
        Assert.NotNull(reservation.User);
        Assert.NotNull(reservation.Copy.Book);
    }
}
