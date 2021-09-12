using System.Collections.Generic;
using System.Linq;
using GripItemTrade.Core.Domain;
using GripItemTrade.Domain.Customers;

namespace GripItemTrade.Domain.Accounts
{
	public class Account : EntityBase<int>
	{
		public Customer Customer { get; set; }
		public List<BalanceEntry> BalanceEntries { get; set; } = new List<BalanceEntry>();

		public decimal GetTotalAmount()
		{
			var result = BalanceEntries.Sum(be => be.Amount);
			return result;
		}
	}
}
