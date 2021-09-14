namespace GripItemTrade.Application.TransactionOperations
{
	public sealed record TransactionalOperationEntryDto
	{
		public int TransactionOperationEntryId { get; init; }
		public int BalanceEntryId { get; init; }
		public string Code { get; init; }
		public decimal Amount { get; init; }
	}
}
