using GripItemTrade.Application.Handlers;
using GripItemTrade.Application.TransactionOperations.Extensions;
using GripItemTrade.Core.Interfaces;
using GripItemTrade.Core.ResponseContainers;
using GripItemTrade.Domain.Transactions.Interfaces;
using System.Threading.Tasks;

namespace GripItemTrade.Application.TransactionOperations
{
	public sealed class TransactionalOperationsQueryHandler : GenericQueryHandlerBase<GetTransactionalOperationQuery, TransactionalOperationDto>
	{
		private readonly ITransactionalOperationRepository transactionalOperationRepository;

		public TransactionalOperationsQueryHandler(ITransactionalOperationRepository transactionalOperationRepository)
		{
			this.transactionalOperationRepository = transactionalOperationRepository;
		}

		protected override async Task<IResponseContainerWithValue<TransactionalOperationDto>> GetResultAsync(GetTransactionalOperationQuery query)
		{
			var result = new ResponseContainerWithValue<TransactionalOperationDto>();
			var transactionalOperation = await transactionalOperationRepository.GetByIdAsync(query.TransactionalOperationId);

			if (transactionalOperation is null)
				result.AddErrorMessage($"Transactional operation not found by ID {query.TransactionalOperationId}.");

			var transactionalOperationDto = transactionalOperation.ToTransactionalOperationDto();
			result.SetSuccessValue(transactionalOperationDto);
			return result;
		}
	}
}
