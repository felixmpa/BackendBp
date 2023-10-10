using Bp.Transaction.Service.Entities;
using Microsoft.AspNetCore.Mvc;
using Bp.Common;

namespace Bp.Transaction.Service.Controllers
{
    [ApiController]
    [Route("transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly IRepository<Account> accountsRepository;
        private readonly IRepository<TransactionBalance> transactionsRepository;

        public TransactionsController(IRepository<Account> accountsRepository, IRepository<TransactionBalance> transactionsRepository)
        {
            this.accountsRepository = accountsRepository;
            this.transactionsRepository = transactionsRepository;
        }

        // GET transactions
        [HttpGet]
        public async Task<IEnumerable<TransactionBalance>> GetAsync()
        {
            var transactions = (await transactionsRepository
                                    .GetAllAsync())
                                    .Select(transaction => transaction);
            return transactions;
        }

        // GET transactions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionBalance>> GetByIdAsync(int id)
        {
            var transaction = await transactionsRepository.GetAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }


        // POST /transactions
        [HttpPost]
        public async Task<ActionResult<TransactionBalance>> PostAsync(TransactionDto newTransaction)
        {

            var allAccounts = await accountsRepository.GetAllAsync();
            var existingAccount = allAccounts
                                        .Where(a => a.AccountNumber == newTransaction.AccountNumber)
                                        .FirstOrDefault();

            if (existingAccount == null)
                return StatusCode(500, "Error: Account not found");

            TransactionType transactionType = (TransactionType)newTransaction.Type;

            if (transactionType == TransactionType.Deposit)
            {
                if (newTransaction.Amount <= 0)
                    return BadRequest("Error: Deposit must be positive");

                await CreateTransaction(existingAccount, newTransaction.Amount, transactionType);
                return Ok("Successful: deposit transaction completed");
            }
            else if (transactionType == TransactionType.Withdrawal)
            {
                if (newTransaction.Amount >= 0)
                    return BadRequest("Error: Withdrawal must be negative");

                if (Math.Abs(newTransaction.Amount) > existingAccount.AvailableBalance)
                    return BadRequest("Insufficient funds for withdrawal");

                await CreateTransaction(existingAccount, newTransaction.Amount, transactionType);
                return Ok("Successful: Withdraw transaction completed");
            }

            return BadRequest("Error: Invalid transaction type");
        }

        private async Task CreateTransaction(Account existingAccount, decimal amount, TransactionType transactionType)
        {
            var transaction = new TransactionBalance()
            {
                CustomerId = existingAccount.CustomerId,
                AccountId = existingAccount.Id,
                Date = DateTime.UtcNow,
                Amount = amount,
                Balance = existingAccount.AvailableBalance += amount,
                Type = transactionType
            };
            await transactionsRepository.CreateAsync(transaction);
        }

    }
}
