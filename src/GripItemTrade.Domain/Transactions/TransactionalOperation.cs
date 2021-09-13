using System.Collections.Generic;
using GripItemTrade.Core.Domain;
using GripItemTrade.Domain.Accounts;

namespace GripItemTrade.Domain.Transactions
{
	public class TransactionalOperation : EntityBase<int>
	{
		public Account Account { get; init; }
		public decimal Amount { get; init; }
		public TransactionOperationType OperationType { get; init; }
		public List<TransactionOperationEntry> Entries { get; init; } = new List<TransactionOperationEntry>();
	}
}
