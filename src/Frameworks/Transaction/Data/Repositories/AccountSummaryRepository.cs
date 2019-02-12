namespace Transaction.Framework.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using Transaction.Framework.Data.Entities;
    using Transaction.Framework.Data.Interface;

    public class AccountSummaryRepository : IAccountSummaryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<AccountSummaryEntity> _accountSummaryEntity;

        public AccountSummaryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _accountSummaryEntity = _dbContext.Set<AccountSummaryEntity>();
        }

        public async Task<AccountSummaryEntity> Read(int accountNumber)
        {
            return await _accountSummaryEntity.AsNoTracking()
                .FirstOrDefaultAsync(e => e.AccountNumber == accountNumber);
        }
    }
}
