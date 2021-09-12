namespace GripItemTrade.Application.TransactionOperations
{
	public sealed class TransactionOperationEntryDto
	{
		public int TransactionOperationEntryId { get; set; }
		public int BalanceEntryId { get; set; }
		public string Code { get; set; }
		public decimal Amount { get; set; }
	}
}
