using GripItemTrade.Core.Domain;
using System.Collections.Generic;

namespace GripItemTrade.Domain.Accounts
{
	public sealed record BalanceEntryTransferResult : ValueObject
	{
		public ICollection<BalanceEntryTransferItem> ChargedItems { get; init; } = new List<BalanceEntryTransferItem>();
		public ICollection<BalanceEntryTransferItem> DepositedItems { get; init; } = new List<BalanceEntryTransferItem>();
	}
}
