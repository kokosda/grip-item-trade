using GripItemTrade.Core.Domain;
using GripItemTrade.Domain.Accounts;
using System;

namespace GripItemTrade.Domain.Transactions
{
	public class TransactionalOperationEntry : EntityBase<int>
	{
		public BalanceEntry BalanceEntry { get; init; }
		public decimal Amount { get; init; }
		public string BalanceEntryCode { get; set; }

		public static TransactionalOperationEntry Create(BalanceEntry balanceEntry, decimal amount)
		{
			if (balanceEntry is null)
				throw new ArgumentNullException(nameof(balanceEntry));

			var result = new TransactionalOperationEntry
			{
				BalanceEntry = balanceEntry,
				BalanceEntryCode = balanceEntry.Code,
				Amount = amount
			};

			return result;
		}
	}
}
