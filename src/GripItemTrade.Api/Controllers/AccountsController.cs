using GripItemTrade.Application.Accounting;
using GripItemTrade.Core.Handlers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GripItemTrade.Api.Controllers
{
	[Route("api/v1/accounts")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly IGenericCommandHandler<TransferThingsCommand, TransferThingsDto> transferThingsCommandHandler;

		public AccountsController(IGenericCommandHandler<TransferThingsCommand, TransferThingsDto> transferThingsCommandHandler)
		{
			this.transferThingsCommandHandler = transferThingsCommandHandler;
		}

		[HttpPost]
		public async Task<ActionResult> Post(TransferThingsCommand model)
		{
			var responseContainer = await transferThingsCommandHandler.HandleAsync(model);

			if (!responseContainer.IsSuccess)
				return UnprocessableEntity(responseContainer.Messages);

			return Created(new Uri($"/transactionoperations/{responseContainer.Value.TransactionOperationId}", UriKind.Relative), null);
		}
	}
}
