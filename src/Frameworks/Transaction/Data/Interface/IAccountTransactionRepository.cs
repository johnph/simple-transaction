namespace Transaction.Framework.Data.Interface
{
    using System.Threading.Tasks;
    using Transaction.Framework.Data.Entities;

    public interface IAccountTransactionRepository
    {
        Task Create(AccountTransactionEntity accountTransactionEntity, AccountSummaryEntity accountSummaryEntity);
    }
}
