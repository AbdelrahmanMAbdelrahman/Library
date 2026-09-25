using Library.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace Library.Infrastructure.Data.Interceptor;


public sealed class AuditableEntityInterceptor(IUser user,TimeProvider timeProvider):SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    private void UpdateEntities(DbContext? context)
    {
        if (context is null) return;
        DateTimeOffset date = timeProvider.GetUtcNow();
        foreach(EntityEntry<Audit> entry in context.ChangeTracker.Entries<Audit>())
        {
            if(entry.State is EntityState.Modified or EntityState.Added)
            {
                entry.Entity.CreateAt = date;
                entry.Entity.CreatedBy = user.Id;
            }
            entry.Entity.LastModifiedAt = date;
            entry.Entity.LastModifiedBy = user.Id;

            foreach(ReferenceEntry ownedEntry in entry.References)
            {
                if(ownedEntry.EntityEntry.Entity is Audit ownedEntity &&
                   ownedEntry.EntityEntry.State is EntityState.Added or EntityState.Modified)
                {
                    if (ownedEntry.EntityEntry.State is EntityState.Modified or EntityState.Added)
                    {
                        ownedEntity.CreateAt = date;
                        ownedEntity.CreatedBy = user.Id;
                    }
                    ownedEntity.LastModifiedAt = date;
                    ownedEntity.LastModifiedBy = user.Id;
                }
            }
        }
    }
}
