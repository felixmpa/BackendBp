using System.Net.Http;
using System.Net.Http.Json;
using Bp.Client.Service.Dtos;

namespace Bp.Client.Service.Clients
{
    public class AccountClient
    {
        private readonly HttpClient httpClient;

        public AccountClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<AccountDto>> GetAccountByCustomerIdAsync(int customerId, DateTime? startDate, DateTime? endDate)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<AccountDto>>("accounts/customerAccounts?customerId=" + customerId + "&startDate=" + startDate + "&endDate=" + endDate);
        }
    }
}