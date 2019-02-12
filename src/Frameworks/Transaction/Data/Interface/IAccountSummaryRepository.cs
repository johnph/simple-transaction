namespace Transaction.Framework.Data.Interface
{
    using System.Threading.Tasks;
    using Transaction.Framework.Data.Entities;

    public interface IAccountSummaryRepository
    {
        Task<AccountSummaryEntity> Read(int accountNumber);
    }
}
