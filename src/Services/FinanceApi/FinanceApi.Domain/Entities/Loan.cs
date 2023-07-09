using FinanceApi.Domain.SeedWork;

namespace FinanceApi.Domain.Entities;

public class Loan : Entity
{
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    public string UserId { get; private set; }
    public List<Bill> Bills { get; private set; } = new();

    protected Loan()
    {
    }

    public Loan(DateTime date, string description, string userId)
    {
        Date = date;
        Description = description;
        UserId = userId;
    }
}