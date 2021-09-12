using System.Collections.Generic;
using GripItemTrade.Core.Domain;

namespace GripItemTrade.Domain.Transactions
{
	public class TransactionalOperation : EntityBase<int>
	{
		public int AccountId { get; set; }
		public decimal Amount { get; set; }
		public TransactionOperationType OperationType { get; set; }
		public List<TransactionOperationEntry> Entries { get; set; }
	}
}
