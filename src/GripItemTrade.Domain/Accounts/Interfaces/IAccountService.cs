using GripItemTrade.Core.Interfaces;
using GripItemTrade.Domain.Transactions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GripItemTrade.Domain.Accounts.Interfaces
{
	public interface IAccountService
	{
		Task<IResponseContainerWithValue<IReadOnlyList<TransactionalOperation>>> TransferAsync(Account sourceAccount, Account destinationAccount, ICollection<BalanceEntry> balanceEntries);
	}
}
