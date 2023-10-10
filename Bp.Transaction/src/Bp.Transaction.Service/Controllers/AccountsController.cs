using Bp.Transaction.Service.Entities;
using Microsoft.AspNetCore.Mvc;
using Bp.Common;
using AutoMapper;
using Bp.Transaction.Mapper;

namespace Bp.Transaction.Service.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IRepository<Account> accountsRepository;
        private readonly IRepository<TransactionBalance> transactionsRepository;

        private readonly IMapper _mapper;

        public AccountsController(IRepository<Account> accountsRepository, IRepository<TransactionBalance> transactionsRepository, IMapper mapper)
        {
            this.accountsRepository = accountsRepository;
            this.transactionsRepository = transactionsRepository;
            this._mapper = mapper;
        }

        // GET accounts/
        [HttpGet]
        public async Task<IEnumerable<Account>> GetAsync()
        {
            var accounts = (await accountsRepository
                                    .GetAllAsync())
                                    .Select(account => account);
            return accounts;
        }

        // GET accounts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetByIdAsync(int id)
        {
            var account = await accountsRepository.GetAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // POST /accounts
        [HttpPost]
        public async Task<ActionResult<Account>> PostAsync(Account newAccount)
        {

            if (newAccount.InitialBalance > 0)
                newAccount.AvailableBalance = newAccount.InitialBalance;

            await accountsRepository.CreateAsync(newAccount);

            var createdAccount = await accountsRepository.GetAsync(newAccount.Id);

            if (createdAccount != null)
            {
                return StatusCode(201, createdAccount);
            }

            return StatusCode(500, "Internal server error: Failed to create account");
        }

        // PUT /accounts/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Account>> PutAsync(int id, Account updatedAccount)
        {
            var existingAccount = await accountsRepository.GetAsync(id);

            if (existingAccount == null)
            {
                return NotFound();
            }

            existingAccount.CustomerId = updatedAccount.CustomerId;
            existingAccount.AccountNumber = updatedAccount.AccountNumber;
            existingAccount.Status = updatedAccount.Status;
            existingAccount.AccountType = updatedAccount.AccountType;
            //JIC: Limit critical changes directly to account
            //existingAccount.InitialBalance = updatedAccount.InitialBalance;

            await accountsRepository.UpdateAsync(existingAccount);

            return Ok();
        }

        // DELETE /accounts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var account = await accountsRepository.GetAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            await accountsRepository.RemoveAsync(account.Id);

            return Ok();
        }

        [HttpGet("customerAccounts")]
        public async Task<IEnumerable<AccountDto>> GetAccountByCustomerIdAsync(int customerId, DateTime? startDate, DateTime? endDate)
        {
            var accounts = (await accountsRepository.GetAllAsync())
                                    .Where(a => a.CustomerId == customerId)
                                    .ToList();

            var accountDtos = accounts.Select(account => _mapper.Map<AccountDto>(account)).ToList();

            // Manually loading transactions for each account
            foreach (var account in accountDtos)
            {
                var transactions = (await transactionsRepository.GetAllAsync())
                                   .Where(t => t.AccountId == account.Id)
                                   .Select(t => _mapper.Map<TransactionBalanceDto>(t));

                if (startDate.HasValue)
                {
                    transactions = transactions.Where(a => a.Date >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    transactions = transactions.Where(a => a.Date <= endDate.Value);
                }

                account.Transactions = transactions.ToList();
            }

            return accountDtos;
        }


    }

}