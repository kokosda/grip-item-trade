using GripItemTrade.Core.Domain;
using GripItemTrade.Domain.Accounts;

namespace GripItemTrade.Domain.Transactions
{
	public class TransactionOperationEntry : EntityBase<int>
	{
		public BalanceEntry BalanceEntry { get; set; }
		public decimal Amount { get; set; }
	}
}
