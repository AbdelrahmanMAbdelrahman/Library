using Library.Domain.Reservations;

namespace Library.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Book> Books { get; }
    DbSet<Copy> Copies { get; }
    DbSet<UploadedFile> UploadedFiles { get; }
    DbSet<BorrowingRecord> BorrowingRecords { get; }
    DbSet<Fine> Fines { get; }
    DbSet<Reservation> Reservations { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}
