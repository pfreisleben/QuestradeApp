namespace Shared.Responses.Loans;

public record LoanResponse()
{
    public DateTime Date { get; set; }
    public int Id { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public List<BillDto> Bills { get;  set; } = new();
}

public record BillDto()
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public bool Payed { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? PaymentDay { get; set; }
}