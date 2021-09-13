using GripItemTrade.Application.Handlers;
using GripItemTrade.Application.TransactionOperations;
using GripItemTrade.Core.Interfaces;
using GripItemTrade.Core.ResponseContainers;
using GripItemTrade.Domain.Accounts;
using GripItemTrade.Domain.Accounts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GripItemTrade.Application.Accounting
{
	public sealed class TransferThingsCommandHandler : GenericCommandHandlerBase<TransferThingsCommand, TransferThingsDto>
	{
		private readonly IGenericRepository genericRepository;
		private readonly IAccountService accountService;

		public TransferThingsCommandHandler(IGenericRepository genericRepository, IAccountService accountService)
		{
			this.genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
			this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
		}

		protected override async Task<IResponseContainerWithValue<TransferThingsDto>> GetResultAsync(TransferThingsCommand command)
		{
			var result = new ResponseContainerWithValue<TransferThingsDto>();
			var validationResponseContainer = await ValidateCommandAsync(command);

			result.JoinWith(validationResponseContainer);

			if (!validationResponseContainer.IsSuccess)
				return result;

			var (sourceAccount, destinationAccount, transferItems) = validationResponseContainer.Value;
			var transactionOperationResponseContainer = await accountService.TransferAsync(sourceAccount, destinationAccount, transferItems);
			result.JoinWith(transactionOperationResponseContainer);
			
			if (transactionOperationResponseContainer.IsSuccess)
			{
				var value = new TransferThingsDto
				{
					TransactionOperations = transactionOperationResponseContainer.Value.Select(to => new TransactionOperationDto { TransactionOperationId = to.Id }).ToArray()
				};

				result.SetSuccessValue(value);
			}

			return result;
		}

		private async Task<IResponseContainerWithValue<Tuple<Account, Account, List<BalanceEntryTransferItem>>>> ValidateCommandAsync(TransferThingsCommand command)
		{
			var result = new ResponseContainerWithValue<Tuple<Account, Account, List<BalanceEntryTransferItem>>>();
			var sourceAccount = await genericRepository.GetAsync<Account, int>(command.SourceAccountId);

			if (sourceAccount is null)
			{
				result.AddErrorMessage($"Source account is not found by ID {command.SourceAccountId}.");
				return result;
			}

			var destinationAccount = await genericRepository.GetAsync<Account, int>(command.DestinationAccountId);

			if (destinationAccount is null)
			{
				result.AddErrorMessage($"Destination account is not found by ID {command.DestinationAccountId}.");
				return result;
			}

			var balanceEntries = new List<BalanceEntryTransferItem>();

			foreach (var balanceEntryDto in command.BalanceEntries)
			{
				var balanceEntry = await genericRepository.GetAsync<BalanceEntry, int>(balanceEntryDto.BalanceEntryId);

				if (balanceEntry is null)
				{
					result.AddErrorMessage($"Balance entry is not found by ID {balanceEntryDto.BalanceEntryId}.");
					return result;
				}

				balanceEntries.Add(new BalanceEntryTransferItem { BalanceEntry = balanceEntry, Amount = balanceEntryDto.Amount });
			}

			if (result.IsSuccess)
				result.SetSuccessValue(Tuple.Create(sourceAccount, destinationAccount, balanceEntries));

			return result;
		}
	}
}
