using MediatR;

namespace FinanceApi.Domain.DomainEvents;

public record BillPayed(int BillId, DateTime PaymentDay, string UserId) : INotification;