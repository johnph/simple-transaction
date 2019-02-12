namespace Transaction.Framework.Services.Interface
{
    using System.Threading.Tasks;
    using Transaction.Framework.Domain;

    public interface ITransactionService
    {
        Task<TransactionResult> Balance(int accountNumber);
        Task<TransactionResult> Deposit(AccountTransaction accountTransaction);
        Task<TransactionResult> Withdraw(AccountTransaction accountTransaction);
    }
}
