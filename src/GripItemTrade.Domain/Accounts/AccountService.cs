using GripItemTrade.Domain.Accounts.Interfaces;
using GripItemTrade.Domain.Transactions;
using System;
using System.Collections.Generic;

namespace GripItemTrade.Domain.Accounts
{
	public sealed class AccountService : IAccountService
	{
		public IReadOnlyList<TransactionalOperation> Transfer(Account sourceAccount, Account destinationAccount, ICollection<BalanceEntry> balanceEntries)
		{
			throw new NotImplementedException();
		}
	}
}
