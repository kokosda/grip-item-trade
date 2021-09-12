using GripItemTrade.Core.Domain;
using GripItemTrade.Domain.Customers;

namespace GripItemTrade.Domain.Accounts
{
	public class BalanceEntry : EntityBase<int>
	{
		public Account Account { get; set; }
		public Customer Customer { get; set; }
		public string Code { get; set; }
		public decimal Amount { get; set; }
	}
}
