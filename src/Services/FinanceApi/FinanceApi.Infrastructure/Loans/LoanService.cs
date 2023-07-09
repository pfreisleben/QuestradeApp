using FinanceApi.Application.Loans.Contracts;
using FinanceApi.Domain.Entities;
using FinanceApi.Infrastructure.Persistence.AppDb;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using Shared.Logs.Services;
using Shared.Requests.Finance;

namespace FinanceApi.Infrastructure.Loans;

public class LoanService : ILoanService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogServices _logServices;

    public LoanService(ApplicationDbContext context, ILogServices logServices)
    {
        _context = context;
        _logServices = logServices;
    }

    public async Task<CommandResult> CreateLoan(NewLoanRequest request)
    {
        var loan = new Loan(DateTime.Now, request.Description, request.UserId);

        foreach (var parcela in Enumerable.Range(1, request.NumberOfPortions))
        {
            var dueDate = DateTime.Now.AddDays(parcela * 30);
            var bill = new Bill(
                request.TotalAmount / request.NumberOfPortions,
                false,
                dueDate
            );
            
            loan.Bills.Add(bill);
        }

        await _context.AddAsync(loan);
        await _context.SaveChangesAsync();

        return await CommandResult.SuccessAsync($"Loan created successfully!");
    }
    public async Task<CommandResult<List<Loan>>> ListLoans(string userId)
    {
        var userLoans = await _context.Loans.AsNoTracking()
            .Where(x => x.UserId == userId)
            .Include(x => x.Bills)
            .ToListAsync();

        return await CommandResult<List<Loan>>.SuccessAsync(userLoans);
    }

    public async Task<CommandResult> PayBill(PayBillRequest request)
    {
        var bill = await _context.Bills
            .FirstOrDefaultAsync(x => x.Id == request.BillId);

        bill.Pay(request.PaymentDay, request.UserId);

        await _context.SaveChangesAsync();

        return await CommandResult.SuccessAsync($"Bill payed successfully!");
    }
}