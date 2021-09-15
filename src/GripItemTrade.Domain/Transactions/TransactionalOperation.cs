using System;
using System.Collections.Generic;
using GripItemTrade.Core.Domain;
using GripItemTrade.Core.Interfaces;
using GripItemTrade.Core.ResponseContainers;
using GripItemTrade.Domain.Accounts;

namespace GripItemTrade.Domain.Transactions
{
	public class TransactionalOperation : EntityBase<int>
	{
		public Account Account { get; init; }
		public decimal Amount { get; init; }
		public TransactionalOperationType OperationType { get; init; }
		public TransactionalOperation ParentOperation { get; init; }
		public List<TransactionalOperationEntry> Entries { get; init; } = new List<TransactionalOperationEntry>();
		public IReadOnlyList<TransactionalOperation> DependentOperations { get; init; } = new List<TransactionalOperation>();

		public static IResponseContainerWithValue<TransactionalOperation> Create(
			Account account, 
			TransactionalOperationType operationType,
			ICollection<BalanceEntryTransferItem> transferItems,
			TransactionalOperation parentOperation = null
		)
		{
			if (account is null)
				throw new ArgumentNullException(nameof(account));
			if (transferItems is null)
				throw new ArgumentNullException(nameof(transferItems));

			var result = new ResponseContainerWithValue<TransactionalOperation>();

			if (transferItems.Count == 0)
			{
				result.AddErrorMessage($"{nameof(transferItems)} collection is required to contain elements.");
				return result;
			}

			var transferAmount = 0M;
			var operationEntries = new List<TransactionalOperationEntry>();

			foreach (var transferItem in transferItems)
			{
				transferAmount += transferItem.Amount;

				var operationEntry = TransactionalOperationEntry.Create(transferItem.BalanceEntry, transferItem.Amount);

				operationEntries.Add(operationEntry);
			}

			var transactionalOperation = new TransactionalOperation
			{
				Account = account,
				OperationType = operationType,
				Amount = transferAmount,
				Entries = operationEntries,
				ParentOperation = parentOperation
			};

			result.SetSuccessValue(transactionalOperation);
			return result;
		}
	}
}
