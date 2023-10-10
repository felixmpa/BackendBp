using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
using Bp.Common;

namespace Bp.Transaction.Service.Entities
{
    public class TransactionBalance : IEntity
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
    }

    [NotMapped]
    public class TransactionBalanceDto : TransactionBalance
    {

    }

    [NotMapped]
    public class TransactionDto
    {
        public string AccountNumber { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TransactionType
    {
        Deposit = 1,
        Withdrawal = 2
    }
}