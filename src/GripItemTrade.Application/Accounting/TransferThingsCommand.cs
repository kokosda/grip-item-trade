using System.ComponentModel.DataAnnotations;

namespace GripItemTrade.Application.Accounting
{
	public sealed class TransferThingsCommand
	{
		public BalanceEntryDto[] BalanceEntries { get; set; }

		[Range(1, int.MaxValue)]
		public int SourceAccountId { get; set; }

		[Range(1, int.MaxValue)]
		public int DestinationAccountId { get; set; }
	}
}
