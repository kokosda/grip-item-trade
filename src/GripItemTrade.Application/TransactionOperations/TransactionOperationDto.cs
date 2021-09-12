using System.Collections.Generic;

namespace GripItemTrade.Application.TransactionOperations
{
	public sealed class TransactionOperationDto
	{
		public int TransactionOperationId { get; set; }
		public decimal Amount { get; set; }
		public List<TransactionOperationEntryDto> Entries { get; set; } = new List<TransactionOperationEntryDto>();
	}
}
