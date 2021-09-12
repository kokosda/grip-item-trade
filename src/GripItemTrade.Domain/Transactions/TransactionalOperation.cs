using System.Collections.Generic;
using GripItemTrade.Core.Domain;
using GripItemTrade.Domain.Accounts;

namespace GripItemTrade.Domain.Transactions
{
	public class TransactionalOperation : EntityBase<int>
	{
		public Account Account { get; set; }
		public decimal Amount { get; set; }
		public TransactionOperationType OperationType { get; set; }
		public List<TransactionOperationEntry> Entries { get; set; } = new List<TransactionOperationEntry>();
	}
}
