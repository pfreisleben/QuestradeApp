using FinanceApi.Domain.Entities;
using Shared.Entities;

namespace FinanceApi.Application.Bills.Contracts;

public interface IBillService
{
    Task<CommandResult<Bill>> GetBillById(int billId);
}