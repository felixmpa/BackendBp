using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
using Bp.Common;

namespace Bp.Transaction.Service.Entities
{
    public class Account : IEntity
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(12)]
        public string AccountNumber { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal InitialBalance { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal AvailableBalance { get; set; }

        [Required]
        public AccountStatus Status { get; set; }
    }

    [NotMapped]
    public class AccountDto : Account
    {
        public List<TransactionBalanceDto> Transactions { get; set; } = new List<TransactionBalanceDto>();
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountType
    {
        Checking = 1,
        Savings = 2,
        Credit = 3,
        Current = 4
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountStatus
    {
        Inactive = 0,
        Active = 1,
        Closed = 2
    }
}