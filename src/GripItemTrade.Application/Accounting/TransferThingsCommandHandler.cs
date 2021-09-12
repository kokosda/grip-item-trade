using GripItemTrade.Application.Handlers;
using GripItemTrade.Core.Interfaces;
using GripItemTrade.Core.ResponseContainers;
using GripItemTrade.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GripItemTrade.Application.Accounting
{
	public sealed class TransferThingsCommandHandler : GenericCommandHandlerBase<TransferThingsCommand, TransferThingsDto>
	{
		private readonly IGenericRepository<Account, int> accountRepository;
		private readonly IGenericRepository<BalanceEntry, int> balanceEntryRepository;

		public TransferThingsCommandHandler(IGenericRepository<Account, int> accountRepository, IGenericRepository<BalanceEntry, int> balanceEntryRepository)
		{
			this.accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
			this.balanceEntryRepository = balanceEntryRepository ?? throw new ArgumentNullException(nameof(balanceEntryRepository));
		}

		protected override async Task<IResponseContainerWithValue<TransferThingsDto>> GetResultAsync(TransferThingsCommand command)
		{
			var result = new ResponseContainerWithValue<TransferThingsDto>();
			var sourceAccount = await accountRepository.GetAsync(command.SourceAccountId);

			if (sourceAccount is null)
			{
				result.AddErrorMessage($"Source account is not found by ID {command.SourceAccountId}.");
				return result;
			}

			var destinationAccount = await accountRepository.GetAsync(command.DestinationAccountId);

			if (destinationAccount is null)
			{
				result.AddErrorMessage($"Destination account is not found by ID {command.DestinationAccountId}.");
				return result;
			}

			var balanceEntries = new List<BalanceEntry>();

			foreach (var balanceEntryDto in command.BalanceEntries)
			{
				var balanceEntry = await balanceEntryRepository.GetAsync(balanceEntryDto.BalanceEntryId);
				
				if (balanceEntry is null)
				{
					result.AddErrorMessage($"Balance entry is not found by ID {balanceEntryDto.BalanceEntryId}.");
					return result;
				}

				balanceEntries.Add(balanceEntry);
			}

			throw new NotImplementedException();
		}
	}
}
