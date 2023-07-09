using FinanceApi.Application.Loans.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;
using Shared.Requests.Finance;

namespace FinanceApi.Controllers;

[Route("api/loan/")]
[ApiController]
public class LoanController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoanController(ILoanService loanService)
    {
        _loanService = loanService;
    }


    [HttpGet("GetLoans/{userId}")]
    public async Task<ActionResult<CommandResult>> GetLoans(string userId)
    {
        var retorno = await _loanService.ListLoans(userId);

        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }

    [HttpPost("AddLoan")]
    public async Task<ActionResult<CommandResult>> AddLoan(NewLoanRequest request)
    {
        var retorno = await _loanService.CreateLoan(request);

        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }

    [HttpPost("PayBill")]
    public async Task<ActionResult<CommandResult>> PayBill(PayBillRequest request)
    {
        var retorno = await _loanService.PayBill(request);

        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }
}