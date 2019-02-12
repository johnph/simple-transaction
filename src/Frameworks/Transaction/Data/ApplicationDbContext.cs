namespace Transaction.Framework.Data
{
    using Microsoft.EntityFrameworkCore;
    using Transaction.Framework.Data.Entities;
    using Transaction.Framework.Data.EntityConfigurations;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AccountSummaryEntityConfiguration
                .Configure(modelBuilder.Entity<AccountSummaryEntity>());

            AccountTransactionEntityConfiguration
                .Configure(modelBuilder.Entity<AccountTransactionEntity>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
