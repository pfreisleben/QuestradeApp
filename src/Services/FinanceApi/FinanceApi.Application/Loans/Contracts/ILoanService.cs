using FinanceApi.Domain.Entities;
using Shared.Entities;
using Shared.Requests.Finance;

namespace FinanceApi.Application.Loans.Contracts;

public interface ILoanService
{
    Task<CommandResult> CreateLoan(NewLoanRequest request);
    Task<CommandResult<List<Loan>>> ListLoans(string userId);
    Task<CommandResult> PayBill(PayBillRequest request);
}