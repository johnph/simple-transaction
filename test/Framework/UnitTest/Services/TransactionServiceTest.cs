namespace Transaction.Framework.UnitTest.Services
{
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System.Threading.Tasks;
    using Transaction.Framework.Data.Entities;
    using Transaction.Framework.Data.Interface;
    using Transaction.Framework.Domain;
    using Transaction.Framework.Exceptions;
    using Transaction.Framework.Mappers;
    using Transaction.Framework.Services;
    using Xunit;

    public class TransactionServiceTest
    {
        protected TransactionService TransactionServiceUnderTest { get; }
        protected Mock<IAccountSummaryRepository> AccountSummaryRepositoryMock { get; }
        protected Mock<IAccountTransactionRepository> AccountTransactionRepositoryMock { get; }
        protected Mock<ILogger<TransactionService>> LoggerMock { get; }
        protected MapperConfiguration MappingConfig { get; }
        protected IMapper Mapper { get; }

        public TransactionServiceTest()
        {
            AccountSummaryRepositoryMock = new Mock<IAccountSummaryRepository>();
            AccountTransactionRepositoryMock = new Mock<IAccountTransactionRepository>();
            LoggerMock = new Mock<ILogger<TransactionService>>();
            MappingConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            Mapper = MappingConfig.CreateMapper();
            TransactionServiceUnderTest = new TransactionService(AccountSummaryRepositoryMock.Object, AccountTransactionRepositoryMock.Object, Mapper, LoggerMock.Object);
        }

        public class Balance : TransactionServiceTest
        {
            [Fact]
            public void Should_return_accountbalance_when_accountnumber_exist()
            {
                // Arange
                int accountNumber = 29489324;
                var accountSummary = new AccountSummaryEntity() {
                    AccountNumber = accountNumber,
                    Balance = 45000,
                    Currency = "INR"
                };

                AccountSummaryRepositoryMock
                    .Setup(i => i.Read(It.IsAny<int>()))
                    .ReturnsAsync(accountSummary);

                // Act
                var result = TransactionServiceUnderTest.Balance(accountNumber).Result;

                // Assert
                Assert.Equal(accountSummary.Balance, result.Balance.Amount);
            }

            [Fact]
            public void Should_throw_transactionexception_when_accountnumber_notexist()
            {
                // Arange
                int accountNumber = 29489324;
                AccountSummaryEntity accountSummary = null;

                AccountSummaryRepositoryMock
                    .Setup(i => i.Read(It.IsAny<int>()))
                    .ReturnsAsync(accountSummary);

                // Act
                var result = TransactionServiceUnderTest.Balance(accountNumber);

                // Assert
                Assert.ThrowsAsync<InvalidAccountNumberException>(async () => await result);
            }
        }
     
        public class Deposit : TransactionServiceTest
        {
            [Fact]
            public void Should_return_transactionresult_on_successfulldeposit()
            {
                // Arrange
                var accountSummary = new AccountSummaryEntity()
                {
                    AccountNumber = 1253667,
                    Balance = 55000,
                    Currency = "INR"
                };

                var accountTransaction = new AccountTransaction()
                {
                    AccountNumber = 1253667,
                    TransactionType = Types.TransactionType.Deposit,
                    Amount = new Types.Money(1000, Types.Currency.INR)
                };

                AccountTransactionRepositoryMock
                    .Setup(i => i.Create(It.IsAny<AccountTransactionEntity>(), It.IsAny<AccountSummaryEntity>()))
                    .Returns(Task.CompletedTask);

                AccountSummaryRepositoryMock
                    .Setup(i => i.Read(It.IsAny<int>()))
                    .ReturnsAsync(accountSummary);

                // Act
                var result = TransactionServiceUnderTest.Deposit(accountTransaction).Result;

                // Assert
                Assert.Equal(accountSummary.Balance, result.Balance.Amount);
            }

            [Fact]
            public void Should_throw_transactionexception__when_transactiondetails_are_invalid()
            {
                // Arrange
                var accountSummary = new AccountSummaryEntity()
                {
                    AccountNumber = 1253667,
                    Balance = 55000,
                    Currency = "INR"
                };

                var accountTransaction = new AccountTransaction()
                {
                    AccountNumber = 1253667,
                    TransactionType = Types.TransactionType.Deposit,
                    Amount = new Types.Money(0, Types.Currency.INR)
                };

                AccountTransactionRepositoryMock
                    .Setup(i => i.Create(It.IsAny<AccountTransactionEntity>(), It.IsAny<AccountSummaryEntity>()))
                    .Returns(Task.CompletedTask);

                AccountSummaryRepositoryMock
                    .Setup(i => i.Read(It.IsAny<int>()))
                    .ReturnsAsync(accountSummary);

                // Act
                var result = TransactionServiceUnderTest.Deposit(accountTransaction);

                // Assert
                Assert.ThrowsAsync<InvalidAmountException>(async () => await result);
            }
        }

        public class Withdraw : TransactionServiceTest
        {
            [Fact]
            public void Should_return_transactionresult_on_successfullwithdraw()
            {
                // Arrange
                var accountSummary = new AccountSummaryEntity()
                {
                    AccountNumber = 1253667,
                    Balance = 55000,
                    Currency = "INR"
                };

                var accountTransaction = new AccountTransaction()
                {
                    AccountNumber = 1253667,
                    TransactionType = Types.TransactionType.Withdrawal,
                    Amount = new Types.Money(1000, Types.Currency.INR)
                };

                AccountTransactionRepositoryMock
                    .Setup(i => i.Create(It.IsAny<AccountTransactionEntity>(), It.IsAny<AccountSummaryEntity>()))
                    .Returns(Task.CompletedTask);

                AccountSummaryRepositoryMock
                    .Setup(i => i.Read(It.IsAny<int>()))
                    .ReturnsAsync(accountSummary);

                // Act
                var result = TransactionServiceUnderTest.Withdraw(accountTransaction).Result;

                // Assert
                Assert.Equal(accountSummary.Balance, result.Balance.Amount);
            }

            [Fact]
            public void Should_throw_transactionexception__when_transactiondetails_are_invalid()
            {
                // Arrange
                var accountSummary = new AccountSummaryEntity()
                {
                    AccountNumber = 1253667,
                    Balance = 55000,
                    Currency = "INR"
                };

                var accountTransaction = new AccountTransaction()
                {
                    AccountNumber = 1253667,
                    TransactionType = Types.TransactionType.Withdrawal,
                    Amount = new Types.Money(65000, Types.Currency.INR)
                };

                AccountTransactionRepositoryMock
                    .Setup(i => i.Create(It.IsAny<AccountTransactionEntity>(), It.IsAny<AccountSummaryEntity>()))
                    .Returns(Task.CompletedTask);

                AccountSummaryRepositoryMock
                    .Setup(i => i.Read(It.IsAny<int>()))
                    .ReturnsAsync(accountSummary);

                // Act
                var result = TransactionServiceUnderTest.Withdraw(accountTransaction);

                // Assert
                Assert.ThrowsAsync<InsufficientBalanceException>(async () => await result);
            }
        }
    }
}
