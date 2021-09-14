using GripItemTrade.Core.Interfaces;
using GripItemTrade.Core.ResponseContainers;
using System;

namespace GripItemTrade.Domain.Accounts.Specifications
{
	internal sealed class CanAmountBeAssignedToBalanceEntrySpecification : ISpecification<BalanceEntry, int>
	{
		private readonly decimal amount;

		public CanAmountBeAssignedToBalanceEntrySpecification(decimal amount)
		{
			this.amount = amount;
		}

		public IResponseContainer IsSatisfiedBy(BalanceEntry balanceEntry)
		{
			if (balanceEntry is null)
				throw new ArgumentNullException(nameof(balanceEntry));

			var result = new ResponseContainer();

			if (amount < 0M)
				result.AddErrorMessage($"Amount can not be negative. Amount passed is {amount}. Balance entry ID {balanceEntry.Id}.");

			return result;
		}
	}
}
