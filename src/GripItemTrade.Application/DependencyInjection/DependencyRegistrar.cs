using GripItemTrade.Application.Accounting;
using GripItemTrade.Application.TransactionOperations;
using GripItemTrade.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace GripItemTrade.Application.DependencyInjection
{
	public static class DependencyRegistrar
	{
		public static IServiceCollection AddApplicationLevelServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddScoped<IGenericCommandHandler<TransferThingsCommand, TransferThingsDto>, TransferThingsCommandHandler>();
			serviceCollection.AddScoped<IGenericQueryHandler<GetTransactionOperationQuery, TransactionOperationDto>, TransactionOperationsQueryHandler>();
			return serviceCollection;
		}
	}
}
