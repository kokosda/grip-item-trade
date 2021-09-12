using System.Collections.Generic;
using System.Linq;
using GripItemTrade.Core.Domain;
using GripItemTrade.Domain.Customers;

namespace GripItemTrade.Domain.Balances
{
	public class Account : EntityBase<int>
	{
		public Customer Customer { get; set; }
		public List<BalanceEntry> BalanceEntry { get; set; }

		public decimal GetTotalAmount()
		{
			var result = BalanceEntry.Sum(be => be.Amount);
			return result;
		}
	}
}
