using GripItemTrade.Core.Domain;

namespace GripItemTrade.Domain.Accounts
{
	public sealed class BalanceEntryTransferItem : ValueObject
	{
		public BalanceEntry BalanceEntry { get; set; }
		public decimal Amount { get; set; }
	}
}
