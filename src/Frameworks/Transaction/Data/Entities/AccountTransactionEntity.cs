using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transaction.Framework.Data.Entities
{
    [Table("AccountTransaction")]
    public class AccountTransactionEntity
    {
        [Key] public int TransactionId { get; set; }
        [ForeignKey("AccountNumber")] public int AccountNumber { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public AccountSummaryEntity AccountSummary { get; set; }
    }
}
