using GripItemTrade.Domain.Transactions;
using System;
using System.Linq;

namespace GripItemTrade.Application.TransactionOperations.Extensions
{
	public static class TransactionalOperationExtensions
	{
		public static TransactionalOperationDto ToTransactionalOperationDto(this TransactionalOperation transactionalOperation)
		{
			if (transactionalOperation is null)
				throw new ArgumentNullException(nameof(transactionalOperation));

			var result = new TransactionalOperationDto
			{
				TransactionalOperationId = transactionalOperation.Id,
				Amount = transactionalOperation.Amount,
				OperationType = transactionalOperation.OperationType.ToString(),
				Entries = transactionalOperation.Entries.Select(toe => toe.ToTransactionalOperationEntryDto()).ToArray()
			};

			return result;
		}
	}
}
