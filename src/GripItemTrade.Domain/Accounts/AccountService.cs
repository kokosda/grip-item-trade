using GripItemTrade.Core.Interfaces;
using GripItemTrade.Core.ResponseContainers;
using GripItemTrade.Domain.Accounts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GripItemTrade.Domain.Accounts
{
	public sealed class AccountService : IAccountService
	{
		private readonly IGenericRepository genericRepository;

		public AccountService(IGenericRepository genericRepository)
		{
			this.genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
		}

		public async Task<IResponseContainerWithValue<BalanceEntryTransferResult>> TransferAsync(Account sourceAccount, Account destinationAccount, ICollection<BalanceEntryTransferItem> transferItems)
		{
			if (sourceAccount is null)
				throw new ArgumentNullException(nameof(sourceAccount));
			if (destinationAccount is null)
				throw new ArgumentNullException(nameof(destinationAccount));
			if (transferItems is null || transferItems.Count == 0)
				throw new ArgumentNullException(nameof(transferItems));

			var result = new ResponseContainerWithValue<BalanceEntryTransferResult>();
			var validationResponseContainer = Validate(sourceAccount, destinationAccount, transferItems);

			result.JoinWith(validationResponseContainer);

			if (!result.IsSuccess)
				return result;

			var transferAmount = 0M;
			var chargedItems = new List<BalanceEntryTransferItem>();
			var depositedItems = new List<BalanceEntryTransferItem>();

			foreach (var transferItem in transferItems)
			{
				transferAmount += transferItem.Amount;

				var chargeResponseContainer = sourceAccount.ChargeBalanceEntry(transferItem.BalanceEntry.Code, transferItem.Amount);

				if (!chargeResponseContainer.IsSuccess)
				{
					result.JoinWith(chargeResponseContainer);
					return result;
				}

				await genericRepository.UpdateAsync<BalanceEntry, int>(transferItem.BalanceEntry);

				chargedItems.Add(transferItem);

				var depositResponseContainer = destinationAccount.DepositBalanceEntry(transferItem.BalanceEntry.Code, transferItem.Amount);

				if (depositResponseContainer.IsSuccess)
				{
					result.JoinWith(depositResponseContainer);
					return result;
				}

				var depositedBalanceEntry = depositResponseContainer.Value;

				if (depositedBalanceEntry.Id == 0)
					await genericRepository.CreateAsync<BalanceEntry, int>(depositResponseContainer.Value);
				else
					await genericRepository.UpdateAsync<BalanceEntry, int>(depositedBalanceEntry);

				depositedItems.Add(new BalanceEntryTransferItem { BalanceEntry = depositResponseContainer.Value, Amount = transferItem.Amount });
			}

			var balanceEntryTransferResult = new BalanceEntryTransferResult
			{
				ChargedItems = chargedItems,
				DepositedItems = depositedItems
			};

			result.SetSuccessValue(balanceEntryTransferResult);

			return result;
		}

		private static IResponseContainer Validate(Account sourceAccount, Account destinationAccount, ICollection<BalanceEntryTransferItem> transferItems)
		{
			var result = new ResponseContainer();

			if (sourceAccount.Id == destinationAccount.Id)
			{
				result.AddErrorMessage($"Source and destination ID {sourceAccount.Id} accounts are the same. Please provide different source or destination account.");
				return result;
			}

			var orphanedBalanceEntries = transferItems.Where(ti => ti.BalanceEntry.Account.Id != sourceAccount.Id).ToArray();

			if (orphanedBalanceEntries.Any())
			{
				result.AddErrorMessage($"All balance entries must belong to the source account {sourceAccount.Id}. Orphaned balance entries provided are {orphanedBalanceEntries.Select(ti => new { ti.BalanceEntry.Id, ti.BalanceEntry.Code, AccountId = ti.BalanceEntry.Account.Id })}.");
				return result;
			}

			var exceededAmountBalanceEntries = transferItems.Where(ti => ti.BalanceEntry.Amount < ti.Amount || ti.Amount <= 0M).ToArray();

			if (exceededAmountBalanceEntries.Any())
			{
				result.AddErrorMessage($"All balance entries must have amount greater or equal transferring amount and be a positive decimal number. Exceeded amount entries are {exceededAmountBalanceEntries.Select(ti => new { ti.BalanceEntry.Id, ti.BalanceEntry.Code, ti.BalanceEntry.Amount, TransferAmount = ti.Amount })}.");
				return result;
			}

			return result;
		}
	}
}
