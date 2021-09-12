using VacationRental.Core.Domain;

namespace GripItemTrade.Domain.Balances
{
	public class BalanceEntry : EntityBase<int>
	{
		public int AccountId { get; set; }
		public int CustomerId { get; set; }
		public string Code { get; set; }
		public decimal Amount { get; set; }
	}
}
