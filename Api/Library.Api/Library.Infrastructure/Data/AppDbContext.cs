using Library.Domain.Identity.RefreshTokens;
using Library.Domain.Identity.Roles;
using Library.Domain.Identity.Users;

namespace Library.Infrastructure.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<AppUser, AppRole, string>(options), IAppDbContext
{
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
}
