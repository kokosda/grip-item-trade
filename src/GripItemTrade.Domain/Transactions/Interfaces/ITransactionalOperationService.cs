using GripItemTrade.Core.Interfaces;
using GripItemTrade.Domain.Accounts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GripItemTrade.Domain.Transactions.Interfaces
{
	public interface ITransactionalOperationService
	{
		Task<IResponseContainerWithValue<TransactionalOperation>> SaveOperationsAsync(Account account, TransactionalOperationType operationType, ICollection<BalanceEntryTransferItem> transferItems, TransactionalOperation parentOperation = null);
	}
}
