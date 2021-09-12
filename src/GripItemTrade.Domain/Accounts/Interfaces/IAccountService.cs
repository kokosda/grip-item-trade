using GripItemTrade.Domain.Transactions;
using System.Collections.Generic;

namespace GripItemTrade.Domain.Accounts.Interfaces
{
	public interface IAccountService
	{
		IReadOnlyList<TransactionalOperation> Transfer(Account sourceAccount, Account destinationAccount, ICollection<BalanceEntry> balanceEntries);
	}
}
