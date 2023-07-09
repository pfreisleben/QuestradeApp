using FinanceApi.Application.Bills.Contracts;
using FinanceApi.Domain.Entities;
using FinanceApi.Infrastructure.Persistence.AppDb;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace FinanceApi.Infrastructure.Bills;

public class BillService : IBillService
{
    private readonly ApplicationDbContext _context;

    public BillService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CommandResult<Bill>> GetBillById(int billId)
    {
        var bill = await _context.Bills.AsNoTracking().FirstOrDefaultAsync(x => x.Id == billId);

        if (bill is null)
            return await CommandResult<Bill>.FailAsync("Bill does not exist!");
        return await CommandResult<Bill>.SuccessAsync(bill);
    }
}