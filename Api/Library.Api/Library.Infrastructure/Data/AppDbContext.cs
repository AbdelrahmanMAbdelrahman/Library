using Library.Domain.Common;
using Library.Domain.Identity.RefreshTokens;
using Library.Domain.Identity.Roles;
using Library.Domain.Identity.Users;
using MediatR;

namespace Library.Infrastructure.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator)
    : IdentityDbContext<AppUser,IdentityRole,string>(options), IAppDbContext
{
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct) {
        await DispatchDomainEvents(ct);
       return await base.SaveChangesAsync(ct);
    }

    private async Task DispatchDomainEvents(CancellationToken ct)
    {
        List<Entity> domainEntities = ChangeTracker.Entries()
            .Where(e => e.Entity is Entity baseEntity && baseEntity.DomainEvents.Count > 0)
            .Select(e=>(Entity)e.Entity).ToList();
        List<DomainEvent> domainEvents = domainEntities.SelectMany(e=>e.DomainEvents).ToList();
        foreach(DomainEvent domainEvent in domainEvents)
        {
           await mediator.Publish(domainEvent,ct);
        }
        foreach (Entity entity in domainEntities)
        {
            entity.ClearDomainEvents();
        }
    }
}
