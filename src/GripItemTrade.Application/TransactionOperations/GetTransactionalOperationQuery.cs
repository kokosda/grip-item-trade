using System.ComponentModel.DataAnnotations;

namespace GripItemTrade.Application.TransactionOperations
{
	public sealed record GetTransactionalOperationQuery
	{
		[Range(1, int.MaxValue)]
		public int TransactionalOperationId { get; init; }
	}
}
