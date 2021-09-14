using GripItemTrade.Application.TransactionOperations;

namespace GripItemTrade.Application.Accounting
{
	public sealed class TransferThingsDto
	{
		public TransactionalOperationDto[] TransactionalOperations { get; set; } = new TransactionalOperationDto[0];
	}
}
