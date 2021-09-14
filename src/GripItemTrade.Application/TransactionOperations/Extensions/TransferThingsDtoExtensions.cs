using GripItemTrade.Application.Accounting;
using GripItemTrade.Domain.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GripItemTrade.Application.TransactionOperations.Extensions
{
	public static class TransferThingsDtoExtensions
	{
		public static TransferThingsDto ToTransferThingsDto(this ICollection<TransactionalOperation> transactionalOperations)
		{
			if (transactionalOperations is null)
				throw new ArgumentNullException(nameof(transactionalOperations));

			var result = new TransferThingsDto
			{
				TransactionalOperations = transactionalOperations.Select(to => to.ToTransactionalOperationDto()).ToArray()
			};

			return result;
		}
	}
}
