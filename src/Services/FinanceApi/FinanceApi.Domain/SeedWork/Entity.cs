using System.Text.Json.Serialization;
using MediatR;

namespace FinanceApi.Domain.SeedWork;

public abstract class Entity
{
    public int Id { get; protected set; }
    [JsonIgnore]
    private List<INotification> _domainEvents;
    [JsonIgnore]
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();
    
    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents = _domainEvents ?? new List<INotification>();
        _domainEvents.Add(eventItem);
    }
    
    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
    
}