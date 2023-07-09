using FinanceApi.Domain.DomainEvents;
using FinanceApi.Domain.SeedWork;

namespace FinanceApi.Domain.Entities;

public class Bill : Entity
{
    public int LoanId { get;  set; }
    public decimal Value { get; private set; }
    public bool Payed { get;  set; }
    public DateTime DueDate { get; private set; }
    public DateTime? PaymentDay { get;  set; }

    public Bill(int loanId, decimal value, bool payed, DateTime dueDate)
    {
        LoanId = loanId;
        Value = value;
        Payed = payed;
        DueDate = dueDate;
    }

    
    public Bill(decimal value, bool payed, DateTime dueDate)
    {
        Value = value;
        Payed = payed;
        DueDate = dueDate;
    }

    public void Pay(DateTime paymentDay, string userId)
    {
        PaymentDay = paymentDay;
        Payed = true;
        
        AddDomainEvent(new BillPayed(Id, paymentDay, userId));
    }
    
}