using Microsoft.Extensions.DependencyInjection;
using GripItemTrade.Core.Interfaces;
using GripItemTrade.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using GripItemTrade.Infrastructure.DataAccess.Interfaces;
using GripItemTrade.Domain.Accounts;
using GripItemTrade.Domain.Accounts.Interfaces;
using GripItemTrade.Domain.Transactions.Interfaces;
using GripItemTrade.Domain.Transactions;
using GripItemTrade.Infrastructure.DataAccess.Accounting;
using GripItemTrade.Infrastructure.DataAccess.Transactions;

namespace GripItemTrade.Infrastructure.DependencyInjection
{
	public static class DependencyRegistrar
	{
		public static IServiceCollection AddInfrastructureLevelServices(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("GripItemTrade.SqlDb");

			serviceCollection.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString, a => a.MigrationsAssembly("GripItemTrade.Api")));
			serviceCollection.AddScoped<IDbInitializer, DbInitializer>();
			serviceCollection.AddScoped<IGenericRepository, EfGenericRepository>();
			serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

			serviceCollection.AddDomainLevelServices();

			return serviceCollection;
		}

		private static void AddDomainLevelServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddScoped<IAccountService, AccountService>();
			serviceCollection.AddScoped<ITransactionalOperationService, TransactionalOperationService>();
			serviceCollection.AddScoped<IAccountRepository, AccountRepository>();
			serviceCollection.AddScoped<IBalanceEntryRepository, BalanceEntryRepository>();
			serviceCollection.AddScoped<ITransactionalOperationRepository, TransactionalOperationRepository>();
		}
	}
}
