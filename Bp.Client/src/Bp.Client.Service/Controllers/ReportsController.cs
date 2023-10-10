using Bp.Client.Service.Entities;
using Microsoft.AspNetCore.Mvc;
using Bp.Common;
using Bp.Client.Service.Clients;
using Bp.Client.Service.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bp.Client.Service.Controllers
{
    [ApiController]
    [Route("reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IRepository<Customer> customersRepository;
        private readonly AccountClient accountClient;

        public ReportsController(IRepository<Customer> customersRepository, AccountClient accountClient)
        {
            this.customersRepository = customersRepository;
            this.accountClient = accountClient;
        }

        [HttpGet("customerAccounts")]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccountsByCustomerIdAsync(int customerId, DateTime? startDate, DateTime? endDate)
        {

            var accounts = await accountClient.GetAccountByCustomerIdAsync(customerId, startDate, endDate);

            return Ok(accounts);

        }

    }
}
