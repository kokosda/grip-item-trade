using System.Collections.Generic;

namespace GripItemTrade.Application.TransactionOperations
{
	public sealed record TransactionalOperationDto
	{
		public int TransactionalOperationId { get; init; }
		public int AccountId { get; set; }
		public string OperationType { get; init; }
		public decimal Amount { get; init; }
		public TransactionalOperationEntryDto[] Entries { get; set; } = new TransactionalOperationEntryDto[0];
		public TransactionalOperationDto[] DependentOperations { get; set; } = new TransactionalOperationDto[0];
	}
}
