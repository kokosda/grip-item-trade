using System.Collections.Generic;
using System.Linq;
using GripItemTrade.Core.Domain;

namespace GripItemTrade.Domain.Balances
{
	public class Account : EntityBase<int>
	{
		public int CustomerId { get; set; }
		public List<BalanceEntry> BalanceEntry { get; set; }

		public decimal GetTotalAmount()
		{
			var result = BalanceEntry.Sum(be => be.Amount);
			return result;
		}
	}
}
