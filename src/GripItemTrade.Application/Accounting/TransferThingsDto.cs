using GripItemTrade.Application.TransactionOperations;

namespace GripItemTrade.Application.Accounting
{
	public sealed class TransferThingsDto
	{
		public TransactionOperationDto[] TransactionOperations { get; set; } = new TransactionOperationDto[0];
	}
}
