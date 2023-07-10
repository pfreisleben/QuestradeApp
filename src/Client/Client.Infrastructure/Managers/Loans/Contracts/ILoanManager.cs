using Shared.Entities;
using Shared.Requests.Finance;
using Shared.Responses.Loans;

namespace Client.Infrastructure.Managers.Loans.Contracts;

public interface ILoanManager
{
    Task<CommandResult> CreateLoan(NewLoanRequest request);
    Task<CommandResult<List<LoanResponse>>> ListLoans(string userId);
    Task<CommandResult> PayBill(PayBillRequest request);
}