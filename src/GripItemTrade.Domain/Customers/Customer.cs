using GripItemTrade.Domain.Balances;
using System.Collections.Generic;
using VacationRental.Core.Domain;

namespace GripItemTrade.Domain.Customers
{
	public class Customer : EntityBase<int>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public List<Account> Accounts { get; set; }
	}
}
