using GripItemTrade.Application.Handlers;
using GripItemTrade.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace GripItemTrade.Application.Accounting
{
	public sealed class TransferThingsCommandHandler : GenericCommandHandlerBase<TransferThingsCommand, TransferThingsDto>
	{
		public TransferThingsCommandHandler()
		{

		}

		protected override Task<IResponseContainerWithValue<TransferThingsDto>> GetResultAsync(TransferThingsCommand command)
		{
			throw new NotImplementedException();
		}
	}
}
