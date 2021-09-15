using GripItemTrade.Application.TransactionOperations;

namespace GripItemTrade.Application.Accounting
{
	public sealed class TransferThingsDto
	{
		public TransactionalOperationDto TransactionalOperation { get; init; }
	}
}
