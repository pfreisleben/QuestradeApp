namespace Shared.Requests.Finance;

public record CreateLoanRequest()
{
    public LoanDto Loan { get; set; }
}

public record LoanDto()
{
    public int Id { get; set; }
    public DateTime Date { get;  set; }
    public string Description { get;  set; }
    public string UserId { get;  set; }
    public List<BillDto> Bills { get;  set; } = new();
}

public record BillDto()
{
    public int Id { get; set; }
    public decimal Value { get;  set; }
    public bool Payed { get;  set; }
    public DateTime DueDate { get;  set; }
    public DateTime PaymentDay { get;  set; }
}