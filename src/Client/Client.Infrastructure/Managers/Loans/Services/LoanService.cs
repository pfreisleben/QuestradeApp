using System.Net.Http.Json;
using Client.Infrastructure.Managers.Loans.Contracts;
using Shared.Constants;
using Shared.Entities;
using Shared.Requests.Finance;
using Shared.Responses.Loans;

namespace Client.Infrastructure.Managers.Loans.Services;

public class LoanService : ILoanService
{
    private readonly HttpClient _httpClient;

    public LoanService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(HttpClientNames.FinanceApi);
    }

    public async Task<CommandResult> CreateLoan(NewLoanRequest request)
    {
        var endpoint = "loan/AddLoan";
        var response = await _httpClient.PostAsJsonAsync(endpoint, request);
        var result = await response.Content.ReadFromJsonAsync<CommandResult>();
        return result;
    }

    public async Task<CommandResult<List<LoanResponse>>> ListLoans(string userId)
    {
        var endpoint = $"loan/getloans/{userId}";
        var response = await _httpClient.GetAsync(endpoint);

        var result = await response.Content.ReadFromJsonAsync<CommandResult<List<LoanResponse>>>();

        return result!;
    }

    public async Task<CommandResult> PayBill(PayBillRequest request)
    {
        var endpoint = $"loan/PayBill";
        var response = await _httpClient.PostAsJsonAsync(endpoint, request);
        var result = await response.Content.ReadFromJsonAsync<CommandResult>();

        return result!;
    }
}