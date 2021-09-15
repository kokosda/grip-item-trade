using GripItemTrade.Core.Interfaces;
using GripItemTrade.Domain.Accounts;
using GripItemTrade.Domain.Customers;
using GripItemTrade.Infrastructure.DataAccess;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GripItemTrade.Infrastructure.Tests
{
	[TestFixture]
	[Category("Integration")]
	public sealed class EfGenericRepositoryIntegrationTests : RepositoryBaseTest
	{
		[TestCase(TestName = "Gets existing entity and returns it.")]
		public async Task GetAsync_ExistingEntity_SuccessfullyReturns()
		{
			// Arrange
			var dataContext = new DataContext(dbContextOptions);
			var genericRepository = new EfGenericRepository(dataContext);
			var unitOfWork = new UnitOfWork(genericRepository);
			var entity = await CreateEntityAsync(genericRepository);

			// Act
			await unitOfWork.CommitAsync();
			var result = await genericRepository.GetAsync<BalanceEntry, int>(entity.Id);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(entity.Id, result.Id);
			await genericRepository.DeleteAsync<BalanceEntry, int>(result.Id);
			await dataContext.DisposeAsync();
		}

		[TestCase(TestName = "Modifies existing entity and verifies concurrent exception.")]
		public async Task ApplyChangesAsync_ExistingEntity_SuccessfullyReturns()
		{
			// Arrange
			var task0 = await Task.Factory.StartNew(async () =>
			{
				var dataContext = new DataContext(dbContextOptions);
				var genericRepository = new EfGenericRepository(dataContext);
				var result = await CreateEntityAsync(genericRepository);
				var unitOfWork = new UnitOfWork(genericRepository);
				await unitOfWork.CommitAsync();
				return result;
			});

			var entity = await task0;

			var task1 = await Task.Factory.StartNew(async () =>
			{
				var dataContext = new DataContext(dbContextOptions);
				var genericRepository = new EfGenericRepository(dataContext);
				var unitOfWork = new UnitOfWork(genericRepository);
				await UpdateEntityAsync(genericRepository, entity, 2M);
				var result = await unitOfWork.CommitAsync();
				return result;
			}, TaskCreationOptions.LongRunning);

			var task2 = await Task.Factory.StartNew(async () =>
			{
				var dataContext = new DataContext(dbContextOptions);
				var genericRepository = new EfGenericRepository(dataContext);
				var unitOfWork = new UnitOfWork(genericRepository);
				await UpdateEntityAsync(genericRepository, entity, 4M);
				var result = await unitOfWork.CommitAsync();
				return result;
			}, TaskCreationOptions.LongRunning);

			// Act
			await Task.WhenAll(task1, task2);
			var dataContext = new DataContext(dbContextOptions);
			var genericRepository = new EfGenericRepository(dataContext);
			await genericRepository.DeleteAsync<BalanceEntry, int>(entity.Id);
			await genericRepository.DeleteAsync<Account, int>(entity.Account.Id);
			await genericRepository.DeleteAsync<Customer, int>(entity.Account.Customer.Id);

			// Assert
			Assert.IsNotNull(task1.Result);
			Assert.IsNotNull(task2.Result);

			if (!task1.Result.IsSuccess)
				Assert.IsNotNull(task1.Result.Messages, task1.Result.Messages);
			else if (!task2.Result.IsSuccess)
				Assert.IsNotNull(task2.Result.Messages, task2.Result.Messages);
		}

		private async Task<BalanceEntry> CreateEntityAsync(EfGenericRepository efGenericRepository)
		{
			var account = new Account { Customer = new Customer() } ;

			await efGenericRepository.CreateAsync<Account, int>(account);

			var balanceEntry = BalanceEntry.Create(account, "ITEM", 10M);

			await efGenericRepository.CreateAsync<BalanceEntry, int>(balanceEntry);

			return balanceEntry;
		}

		private async Task UpdateEntityAsync(EfGenericRepository efGenericRepository, BalanceEntry balanceEntry, decimal value)
		{
			balanceEntry.Charge(value);
			await efGenericRepository.UpdateAsync<BalanceEntry, int>(balanceEntry);
		}
	}
}