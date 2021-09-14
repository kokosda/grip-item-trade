using GripItemTrade.Domain.Transactions;
using System;

namespace GripItemTrade.Application.TransactionOperations.Extensions
{
	public static class TransactionalOperationEntryExtensions
	{
		public static TransactionalOperationEntryDto ToTransactionalOperationEntryDto(this TransactionalOperationEntry transactionalOperationEntry)
		{
			if (transactionalOperationEntry is null)
				throw new ArgumentNullException(nameof(transactionalOperationEntry));

			var result = new TransactionalOperationEntryDto
			{
				TransactionOperationEntryId = transactionalOperationEntry.Id,
				Amount = transactionalOperationEntry.Amount,
				BalanceEntryId = transactionalOperationEntry.Id,
				Code = transactionalOperationEntry.BalanceEntryCode
			};

			return result;
		}
	}
}
