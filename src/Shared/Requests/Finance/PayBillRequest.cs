namespace Shared.Requests.Finance;

public record PayBillRequest
{
    public int BillId { get; set; }
    public DateTime PaymentDay { get; set; }
    public string UserId { get; set; }
}