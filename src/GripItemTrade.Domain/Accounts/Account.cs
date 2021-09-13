using GripItemTrade.Core.Domain;
using GripItemTrade.Core.Interfaces;
using GripItemTrade.Core.ResponseContainers;
using GripItemTrade.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GripItemTrade.Domain.Accounts
{
	public class Account : EntityBase<int>
	{
		public Customer Customer { get; init; }
		public List<BalanceEntry> BalanceEntries { get; init; } = new List<BalanceEntry>();

		public decimal GetTotalAmount()
		{
			var result = BalanceEntries.Sum(be => be.Amount);
			return result;
		}

		public IResponseContainer ChargeBalanceEntry(string balanceEntryCode, decimal amount)
		{
			if (string.IsNullOrWhiteSpace(balanceEntryCode))
				throw new ArgumentException($"'{nameof(balanceEntryCode)}' cannot be null or whitespace.", nameof(balanceEntryCode));

			var result = new ResponseContainer();
			var balanceEntry = BalanceEntries.FirstOrDefault(be => be.Code == balanceEntryCode);

			if (balanceEntry is null)
				result.AddErrorMessage($"Balance entry with code {balanceEntryCode} can not be found.");

			if (result.IsSuccess)
				balanceEntry.Charge(amount);

			return result;
		}

		public IResponseContainerWithValue<BalanceEntry> DepositBalanceEntry(string balanceEntryCode, decimal amount)
		{
			if (string.IsNullOrWhiteSpace(balanceEntryCode))
				throw new ArgumentException($"'{nameof(balanceEntryCode)}' cannot be null or whitespace.", nameof(balanceEntryCode));

			var result = new ResponseContainerWithValue<BalanceEntry>();
			var ownBalanceEntry = BalanceEntries.Where(be => be.Code == balanceEntryCode).FirstOrDefault();

			if (ownBalanceEntry is null)
			{
				ownBalanceEntry = BalanceEntry.Create(this, balanceEntryCode, amount);
				BalanceEntries.Add(ownBalanceEntry);
			}
			else
			{
				var depositResponseContainer = ownBalanceEntry.Deposit(amount);
				result.JoinWith(depositResponseContainer);
			}

			if (result.IsSuccess)
				result.SetSuccessValue(ownBalanceEntry);

			return result;
		}
	}
}
