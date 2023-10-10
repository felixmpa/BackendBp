using System;
using System.Collections.Generic;

namespace Bp.Client.Service.Dtos
{
    public record AccountDto(int customerId, string accountNumber, string accountType, decimal initialBalance, decimal availableBalance, string status, List<TransactionBalanceDto> transactions);

    public record TransactionBalanceDto(int id, int customerId, int accountId, DateTime date, string type, decimal amount, decimal balance);

}