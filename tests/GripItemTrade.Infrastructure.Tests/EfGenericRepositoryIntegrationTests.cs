using GripItemTrade.Domain.Accounts;
using GripItemTrade.Domain.Customers;
using GripItemTrade.Infrastructure.DataAccess;
using NUnit.Framework;
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
			var entity = await CreateEntityAsync(genericRepository);

			// Act
			var account = await genericRepository.GetAsync<Account, int>(entity.Id);

			// Assert
			Assert.IsNotNull(account);
			Assert.AreEqual(entity.Id, account.Id);
			await genericRepository.DeleteAsync<Account, int>(account.Id);
			await dataContext.DisposeAsync();
		}

		private async Task<Account> CreateEntityAsync(EfGenericRepository efGenericRepository)
		{
			var account = new Account
			{
				Customer = new Customer
				{
					FirstName = "Brandon",
					LastName = "Lee"
				}
			};

			await efGenericRepository.CreateAsync<Account, int>(account);
			return account;
		}
	}
}