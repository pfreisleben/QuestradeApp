using FinanceApi.Application.Bills.Contracts;
using FinanceApi.Application.Scores;
using FinanceApi.Domain.DomainEvents;
using MediatR;
using Shared.Logs.Services;
using Shared.Requests.Score;

namespace FinanceApi.Application.Handlers;

public class BillPayedHandler : INotificationHandler<BillPayed>
{
    private readonly IScoreService _scoreService;
    private readonly IBillService _billService;
    private readonly ILogServices _logServices;

    public BillPayedHandler(IScoreService scoreService, IBillService billService, ILogServices logServices)
    {
        _scoreService = scoreService;
        _billService = billService;
        _logServices = logServices;
    }

    public async Task Handle(BillPayed notification, CancellationToken cancellationToken)
    {
        var payedBillResult = await _billService.GetBillById(notification.BillId);
        if (payedBillResult.Succeeded)
        {
            var payedBill = payedBillResult.Data;

            if (payedBill.PaymentDay < payedBill.DueDate)
            {
                await _scoreService.AddScore(new AddScoreRequest(notification.UserId, 1));
            }
            else
            {
               await  _scoreService.AddScore(new AddScoreRequest(notification.UserId, 2));
            }
        }
    }
}