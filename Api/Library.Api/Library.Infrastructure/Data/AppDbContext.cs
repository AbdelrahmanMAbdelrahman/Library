using Library.Domain.Reservations;

namespace Library.Infrastructure.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator)
    : IdentityDbContext<AppUser,IdentityRole,string>(options), IAppDbContext
{
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<Book> Books => Set<Book>();

    public DbSet<Copy> Copies => Set<Copy>();

    public DbSet<UploadedFile> UploadedFiles => Set<UploadedFile>();

    public DbSet<BorrowingRecord> BorrowingRecords =>Set<BorrowingRecord>();

    public DbSet<Fine> Fines => Set<Fine>();

    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
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
