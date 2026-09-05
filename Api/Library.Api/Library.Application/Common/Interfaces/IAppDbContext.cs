using Library.Domain.Identity.RefreshTokens;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<RefreshToken> RefreshTokens { get; }
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
