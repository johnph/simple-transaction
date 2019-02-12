using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transaction.Framework.Data.Entities
{
    [Table("AccountSummary", Schema = "dbo")]
    public class AccountSummaryEntity
    {
        [Key]
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public ICollection<AccountTransactionEntity> AccountTransactions { get; set; }
    }
}
