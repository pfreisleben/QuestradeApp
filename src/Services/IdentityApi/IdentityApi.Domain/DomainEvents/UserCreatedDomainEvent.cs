using MediatR;

namespace IdentityApi.Domain.DomainEvents;

public record UserCreatedDomainEvent(string UserId) : INotification;
