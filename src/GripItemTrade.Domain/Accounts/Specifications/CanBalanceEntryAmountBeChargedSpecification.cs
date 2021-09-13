using GripItemTrade.Core.Interfaces;
using System;

namespace GripItemTrade.Domain.Accounts.Specifications
{
	internal sealed class CanBalanceEntryAmountBeChargedSpecification : ISpecification<BalanceEntry, int>
	{
		private readonly decimal amount;

		public CanBalanceEntryAmountBeChargedSpecification(decimal amount)
		{
			this.amount = amount;
		}

		public IResponseContainer IsSatisfiedBy(BalanceEntry balanceEntry)
		{
			if (balanceEntry is null)
				throw new ArgumentNullException(nameof(balanceEntry));

			var result = new CanAmountBeAssignedToBalanceEntrySpecification(amount).IsSatisfiedBy(balanceEntry);

			if (result.IsSuccess && amount > balanceEntry.Amount)
				result.AddErrorMessage($"Unable to charge amount that exceeds stored amount {balanceEntry.Amount}. Amount passed is {amount}.");

			return result;
		}
	}
}
