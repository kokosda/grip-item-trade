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
		private readonly List<BalanceEntry> balanceEntries = new List<BalanceEntry>();

		public Customer Customer { get; init; }
		public IReadOnlyList<BalanceEntry> BalanceEntries 
		{ 
			get => balanceEntries;
			init => balanceEntries = value is not null ? value.ToList() : throw new ArgumentNullException(nameof(balanceEntries)); 
		}

		public IResponseContainer ChargeBalanceEntry(string balanceEntryCode, decimal amount)
		{
			if (string.IsNullOrWhiteSpace(balanceEntryCode))
				throw new ArgumentException($"'{nameof(balanceEntryCode)}' cannot be null or whitespace.", nameof(balanceEntryCode));

			var result = new ResponseContainer();
			var balanceEntry = balanceEntries.FirstOrDefault(be => be.Code == balanceEntryCode);

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
				ownBalanceEntry = BalanceEntry.Create(this, balanceEntryCode, amount);
			else
			{
				var depositResponseContainer = ownBalanceEntry.Deposit(amount);
				result.JoinWith(depositResponseContainer);
			}

			if (result.IsSuccess)
				result.SetSuccessValue(ownBalanceEntry);

			return result;
		}

		internal void AppendBalanceEntry(BalanceEntry balanceEntry)
		{
			if (balanceEntry is null)
				throw new ArgumentNullException(nameof(balanceEntry));
			if (Id != balanceEntry.Account.Id)
				throw new InvalidOperationException($"Can not reassign balance entry {balanceEntry.Id} to account {Id} it does not belong to.");

			balanceEntries.Add(balanceEntry);
		}
	}
}
