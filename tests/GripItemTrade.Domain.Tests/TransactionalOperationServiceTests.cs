using GripItemTrade.Core.Interfaces;
using GripItemTrade.Domain.Accounts;
using GripItemTrade.Domain.Customers;
using GripItemTrade.Domain.Transactions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GripItemTrade.Domain.Tests
{
	public sealed class TransactionalOperationServiceTests
	{
		private TransactionalOperationService transactionalOperartionService;
		private Mock<IGenericRepository> genericRepositoryMock;

		[SetUp]
		public void Setup()
		{
			var mockRepository = new MockRepository(MockBehavior.Strict);
			genericRepositoryMock = mockRepository.Create<IGenericRepository>();
			transactionalOperartionService = new TransactionalOperationService(genericRepositoryMock.Object);
		}

		[TestCase("Debit", TestName = "Transfers debited amount successfully and returns balance entries.")]
		[TestCase("Credit", TestName = "Transfers credited amount successfully and returns balance entries.")]
		public async Task TransferAsync_FullBalanceTransferEntries_SuccessfullyDepositedAndCredited(TransactionalOperationType operationType)
		{
			// Arrange
			var account = new Account { Id = 1, Customer = new Customer() };
			var balanceEntries = new List<BalanceEntry>
			{
				BalanceEntry.Create(account, code: "CAMERA", amount: 17M),
				BalanceEntry.Create(account, code: "SALT", amount: 4M),
				BalanceEntry.Create(account, code : "BOLT", amount: 3M)
			};
			var transferItems = new[]
			{
				new BalanceEntryTransferItem { BalanceEntry = balanceEntries[0], Amount = 2M },
				new BalanceEntryTransferItem { BalanceEntry = balanceEntries[2], Amount = 1M }
			};

			genericRepositoryMock.Setup(gr => gr.CreateAsync<TransactionalOperation, int>(It.IsAny<TransactionalOperation>()))
				.Returns((TransactionalOperation to) => Task.FromResult(to));

			// Act
			var result = await transactionalOperartionService.SaveOperationsAsync(account, operationType, transferItems);

			// Assert
			Assert.IsTrue(result.IsSuccess);
			Assert.NotNull(result.Value);

			var transactionOperation = result.Value;
			Assert.AreEqual(3M, transactionOperation.Amount);
			Assert.AreEqual(2, transactionOperation.Entries.Count);
			Assert.AreEqual(operationType, transactionOperation.OperationType);
			Assert.AreEqual(2M, transactionOperation.Entries[0].Amount);
			Assert.AreEqual("CAMERA", transactionOperation.Entries[0].BalanceEntry.Code);
			Assert.AreEqual(1M, transactionOperation.Entries[1].Amount);
			Assert.AreEqual("BOLT", transactionOperation.Entries[1].BalanceEntry.Code);
		}
	}
}