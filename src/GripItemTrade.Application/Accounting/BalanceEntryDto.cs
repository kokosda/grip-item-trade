using System;
using System.ComponentModel.DataAnnotations;

namespace GripItemTrade.Application.Accounting
{
	public sealed class BalanceEntryDto
	{
		[Range(1, int.MaxValue)]
		public int BalanceEntryId { get; set; }

		[Range(typeof(decimal), "0.01", "2000000000")]
		public decimal Amount { get; set; }
	}
}
