using GripItemTrade.Core.Domain;

namespace GripItemTrade.Domain.Transactions
{
	public class TransactionOperationEntry : EntityBase<int>
	{
		public int BalanceEntryId { get; set; }
		public decimal Amount { get; set; }
	}
}
