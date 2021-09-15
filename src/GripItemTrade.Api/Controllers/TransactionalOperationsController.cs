using GripItemTrade.Application.TransactionOperations;
using GripItemTrade.Core.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GripItemTrade.Api.Controllers
{
	[Route("api/v1/transactionaloperations")]
	[ApiController]
	public class TransactionalOperationsController : ControllerBase
	{
		private readonly IGenericQueryHandler<GetTransactionalOperationQuery, TransactionalOperationDto> transactionalOperationQueryHandler;

		public TransactionalOperationsController(IGenericQueryHandler<GetTransactionalOperationQuery, TransactionalOperationDto> transactionalOperationQueryHandler)
		{
			this.transactionalOperationQueryHandler = transactionalOperationQueryHandler ?? throw new System.ArgumentNullException(nameof(transactionalOperationQueryHandler));
		}

		[HttpGet]
		[Route("{transactionalOperationId:int}")]
		public async Task<ActionResult> Get([FromRoute] GetTransactionalOperationQuery model)
		{
			var responseContainer = await transactionalOperationQueryHandler.HandleAsync(model);

			if (!responseContainer.IsSuccess)
				return NotFound(responseContainer.Messages);

			return new JsonResult(responseContainer.Value);
		} 
	}
}
