using System.ComponentModel.DataAnnotations;

namespace GripItemTrade.Application.TransactionOperations
{
	public sealed class GetTransactionOperationQuery
	{
		[Range(1, int.MaxValue)]
		public int TransactionOperationId { get; set; }
	}
}
