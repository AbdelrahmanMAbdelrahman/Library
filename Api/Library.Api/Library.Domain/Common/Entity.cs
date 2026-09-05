using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Common
{
    public class Entity
    {
        public Guid Id { get; set; }
        readonly List<DomainEvent> _domainEvents = [];
        [NotMapped]
        public List<DomainEvent> DomainEvents => _domainEvents;
        public Entity(){}
        public Entity(Guid id){ this.Id = id == Guid.Empty ? Guid.NewGuid() : Id; }
        public void AddDomainEvent(DomainEvent domainEvent) { 
        DomainEvents.Add(domainEvent);
        }
        public void RemoveDomainEvent(DomainEvent domainEvent) { 
        DomainEvents.Remove(domainEvent);
        }
        public void ClearDomainEvents() { DomainEvents.Clear(); }
        
    }
}
