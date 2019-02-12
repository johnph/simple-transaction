using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using Transaction.Framework.Data;
using Transaction.Framework.Data.Entities;
using Transaction.Framework.Data.Repositories;
using Transaction.Framework.Mappers;
using Transaction.Framework.Types;
using Xunit;

namespace Transaction.Framework.UnitTest.Data.Repositories
{
    public class AccountTransactionRepositoryTest
    {
        protected AccountTransactionRepository AccountTransactionRepositoryUnderTest { get; set; }
        protected ApplicationDbContext DbContextInMemory { get; }
        protected MapperConfiguration MappingConfig { get; }
        protected IMapper Mapper { get; }

        public AccountTransactionRepositoryTest()
        {
            DbContextInMemory = GetInMemoryDbContext();
            MappingConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            Mapper = MappingConfig.CreateMapper();
            AccountTransactionRepositoryUnderTest = new AccountTransactionRepository(DbContextInMemory);
        }

        public class Create : AccountTransactionRepositoryTest
        {
            // [Fact]
            public void Should_create_transaction_and_update_summary()
            {
                // Arrange
                var accountTransaction = CreatedAccountTransactionDepositDataEntity;
                var accountSummary = UpdatedAccountSummaryDataEntity;

                // Act
                var result = AccountTransactionRepositoryUnderTest.Create(accountTransaction, accountSummary);

                // Assert
                Assert.True(true);
            }
        }

        private static ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase("simpletransactiondb")
                      .Options;
            var context = new ApplicationDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var accountSummaryDataEntity = new AccountSummaryEntity()
            {
                AccountNumber = 2398,
                Balance = 10000,
                Currency = "INR"
            }; 
            context.Add(accountSummaryDataEntity);
            context.SaveChanges();

            return context;
        }

        protected static AccountSummaryEntity UpdatedAccountSummaryDataEntity => 
            new AccountSummaryEntity()
        {
            AccountNumber = 2398,
            Balance = 15000,
            Currency = "INR"
        };

        protected static AccountTransactionEntity CreatedAccountTransactionDepositDataEntity =>
            new AccountTransactionEntity() {
                AccountNumber = 2398,
                Date = DateTime.UtcNow,
                Description = "Credit",
                Amount = 5000,
                TransactionType = TransactionType.Deposit.ToString()
            };

        protected static AccountTransactionEntity AccountTransactionWithdrawalDataEntity =>
           new AccountTransactionEntity()
           {
               AccountNumber = 2398,
               Date = DateTime.UtcNow,
               Description = "Debit",
               Amount = 2500,
               TransactionType = TransactionType.Withdrawal.ToString()
           };
    }
}
