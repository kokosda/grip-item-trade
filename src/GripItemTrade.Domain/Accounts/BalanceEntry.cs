using GripItemTrade.Core.Domain;
using GripItemTrade.Core.Interfaces;
using GripItemTrade.Domain.Accounts.Specifications;
using GripItemTrade.Domain.Customers;
using System;

namespace GripItemTrade.Domain.Accounts
{
	public class BalanceEntry : EntityBase<int>
	{
		private decimal amount;

		public Account Account { get; init; }
		public Customer Customer { get; init; }
		public string Code { get; init; }
		public decimal Amount 
		{ 
			get => amount;
			init
			{
				var response = new CanAmountBeAssignedToBalanceEntrySpecification(value).IsSatisfiedBy(this);

				if (!response.IsSuccess)
					throw new InvalidOperationException(response.Messages);

				amount = value;
			}
		}

		public IResponseContainer Charge(decimal amount)
		{
			var result = new CanBalanceEntryAmountBeChargedSpecification(amount).IsSatisfiedBy(this);

			if (result.IsSuccess)
				this.amount -= amount;

			return result;
		}

		public IResponseContainer Deposit(decimal amount)
		{
			var result = new CanAmountBeAssignedToBalanceEntrySpecification(amount).IsSatisfiedBy(this);

			if (result.IsSuccess)
				this.amount += amount;

			return result;
		}

		public static BalanceEntry Create(Account accout, string code, decimal amount)
		{
			if (accout is null)
				throw new ArgumentNullException(nameof(accout));
			if (accout.Customer is null)
				throw new InvalidOperationException($"Account must have customer property assigned. {accout.Id}");
			if (string.IsNullOrWhiteSpace(code))
				throw new ArgumentException($"'{nameof(code)}' cannot be null or whitespace.", nameof(code));

			var result = new BalanceEntry
			{
				Account = accout,
				Customer = accout.Customer,
				Amount = amount,
				Code = code
			};

			return result;
		}
	}
}
