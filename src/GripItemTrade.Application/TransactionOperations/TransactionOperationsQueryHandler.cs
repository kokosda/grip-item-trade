using GripItemTrade.Application.Handlers;
using GripItemTrade.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace GripItemTrade.Application.TransactionOperations
{
	public sealed class TransactionOperationsQueryHandler : GenericQueryHandlerBase<GetTransactionOperationQuery, TransactionOperationDto>
	{
		public TransactionOperationsQueryHandler()
		{
		}

		protected override Task<IResponseContainerWithValue<TransactionOperationDto>> GetResultAsync(GetTransactionOperationQuery query)
		{
			throw new NotImplementedException();
		}
	}
}
