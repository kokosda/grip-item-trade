using GripItemTrade.Application.TransactionOperations;
using GripItemTrade.Core.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GripItemTrade.Api.Controllers
{
	[Route("api/v1/transactionoperations")]
	[ApiController]
	public class TransactionOperationsController : ControllerBase
	{
		private readonly IGenericQueryHandler<GetTransactionOperationQuery, TransactionOperationDto> transactionOperationQueryHandler;

		public TransactionOperationsController(IGenericQueryHandler<GetTransactionOperationQuery, TransactionOperationDto> transactionOperationQueryHandler)
		{
			this.transactionOperationQueryHandler = transactionOperationQueryHandler;
		}

		[HttpGet]
		public async Task<ActionResult> Get(GetTransactionOperationQuery model)
		{
			var responseContainer = await transactionOperationQueryHandler.HandleAsync(model);

			if (!responseContainer.IsSuccess)
				return UnprocessableEntity(responseContainer.Messages);

			return new JsonResult(responseContainer.Value);
		} 
	}
}
