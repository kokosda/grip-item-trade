using GripItemTrade.Core.Interfaces;
using GripItemTrade.Domain.Accounts.Interfaces;
using GripItemTrade.Domain.Transactions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GripItemTrade.Domain.Accounts
{
	public sealed class AccountService : IAccountService
	{
		/// <summary>
		/// TODO: implement logic of account withdrawal and population. Don't forget about optimistic concurrency.
		/// </summary>
		public Task<IResponseContainerWithValue<IReadOnlyList<TransactionalOperation>>> TransferAsync(Account sourceAccount, Account destinationAccount, ICollection<BalanceEntry> balanceEntries)
		{
			throw new NotImplementedException();
		}
	}
}
