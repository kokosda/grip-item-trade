using GripItemTrade.Core.Interfaces;
using GripItemTrade.Domain.Accounts;
using GripItemTrade.Domain.Customers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GripItemTrade.Domain.Tests
{
	public sealed class AccountsServiceTests
	{
		private AccountService accountsService;
		private Mock<IGenericRepository> genericRepositoryMock;

		[SetUp]
		public void Setup()
		{
			var mockRepository = new MockRepository(MockBehavior.Strict);
			genericRepositoryMock = mockRepository.Create<IGenericRepository>();
			accountsService = new AccountService(genericRepositoryMock.Object);
		}

		[TestCase(TestName = "Transfers successfully and returns balance entries.")]
		public async Task TransferAsync_FullBalanceTransferEntries_SuccessfullyDepositedAndCredited()
		{
			// Arrange
			var sourceAccount = new Account { Id = 1, Customer = new Customer() };
			var sourceBalanceEntries = new List<BalanceEntry>
			{
				BalanceEntry.Create(sourceAccount, code: "STONE", amount: 17M),
				BalanceEntry.Create(sourceAccount, code: "BRIDGE", amount: 10M)
			};

			var destinationAccount = new Account { Id = 2, Customer = new Customer() };

			var destinationBalanceEntries = new List<BalanceEntry>
			{
				BalanceEntry.Create(destinationAccount, code: "BRIDGE", amount: 2M),
				BalanceEntry.Create(destinationAccount, code: "PEN", amount: 7M)
			};

			var transferItems = new[]
			{
				new BalanceEntryTransferItem { BalanceEntry = sourceBalanceEntries[0], Amount = 8M },
				new BalanceEntryTransferItem { BalanceEntry = sourceBalanceEntries[1], Amount = 2M }
			};

			genericRepositoryMock.Setup(gr => gr.UpdateAsync<BalanceEntry, int>(It.IsAny<BalanceEntry>())).Returns(Task.CompletedTask);
			genericRepositoryMock.Setup(gr => gr.CreateAsync<BalanceEntry, int>(It.IsAny<BalanceEntry>())).Returns((BalanceEntry be) => Task.FromResult(be));

			// Act
			var result = await accountsService.TransferAsync(sourceAccount, destinationAccount, transferItems);

			// Assert
			Assert.IsTrue(result.IsSuccess);
			Assert.NotNull(result.Value);

			var transferResult = result.Value;
			Assert.IsNotNull(transferResult.ChargedItems);
			Assert.IsNotNull(transferResult.DepositedItems);
			Assert.AreEqual(transferResult.ChargedItems.Count, transferResult.DepositedItems.Count);
			Assert.AreEqual(9M, sourceBalanceEntries[0].Amount);
			Assert.AreEqual(8M, sourceBalanceEntries[1].Amount);
			Assert.AreEqual(3, destinationAccount.BalanceEntries.Count);
			Assert.AreEqual(4M, destinationBalanceEntries[0].Amount);
			Assert.AreEqual(7M, destinationBalanceEntries[1].Amount);

			var createdBalanceEntry = destinationAccount.BalanceEntries.FirstOrDefault(be => be.Code == "STONE");
			Assert.IsNotNull(createdBalanceEntry);
			Assert.AreEqual(8M, createdBalanceEntry.Amount);
		}
	}
}