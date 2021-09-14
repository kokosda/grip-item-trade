using GripItemTrade.Core.Interfaces;
using GripItemTrade.Core.ResponseContainers;
using GripItemTrade.Domain.Accounts;
using GripItemTrade.Domain.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GripItemTrade.Domain.Transactions
{
	public sealed class TransactionalOperationService : ITransactionalOperationService
	{
		private readonly IGenericRepository genericRepository;

		public TransactionalOperationService(IGenericRepository genericRepository)
		{
			this.genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
		}

		public async Task<IResponseContainerWithValue<TransactionalOperation>> SaveOperationsAsync(Account account, TransactionalOperationType operationType, ICollection<BalanceEntryTransferItem> transferItems)
		{
			if (account is null)
				throw new ArgumentNullException(nameof(account));
			if (transferItems is null)
				throw new ArgumentNullException(nameof(transferItems));

			var result = new ResponseContainerWithValue<TransactionalOperation>();
			var createResponseContainer = TransactionalOperation.Create(account, operationType, transferItems);
			result.JoinWith(createResponseContainer);

			if (!createResponseContainer.IsSuccess)
				return result;

			var transactionalOperation = createResponseContainer.Value;
			await genericRepository.CreateAsync<TransactionalOperation, int>(transactionalOperation);
			result.SetSuccessValue(transactionalOperation);

			return result;
		}
	}
}
