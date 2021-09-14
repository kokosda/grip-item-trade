using GripItemTrade.Application.Handlers;
using GripItemTrade.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace GripItemTrade.Application.TransactionOperations
{
	public sealed class TransactionalOperationsQueryHandler : GenericQueryHandlerBase<GetTransactionalOperationQuery, TransactionalOperationDto>
	{
		public TransactionalOperationsQueryHandler()
		{
		}

		protected override Task<IResponseContainerWithValue<TransactionalOperationDto>> GetResultAsync(GetTransactionalOperationQuery query)
		{
			throw new NotImplementedException();
		}
	}
}
