using GripItemTrade.Application.Accounting;
using NUnit.Framework;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GripItemTrade.Api.Tests
{
	[TestFixture]
	public class PostAccountTransferThingsTests : ApiBaseTest
	{
		[TearDown]
		public void TearDown()
		{
		}

		[TestCase(TestName = "Posts TransferThingsCommand and gets transaction operation ID in the Location header of the response.")]
		public async Task TransferThingsCommand_WhenBalanceEntriesExist_ThenGetTransactionOperationId()
		{
			// Arrange
			var transferThingsCommand = new TransferThingsCommand
			{
				SourceAccountId = 1,
				DestinationAccountId = 2,
				BalanceEntries = new[]
				{
					new BalanceEntryDto
					{
						Amount = 1M,
						BalanceEntryId = 1
					},
					new BalanceEntryDto
					{
						Amount = 2M,
						BalanceEntryId = 2
					}
				}
			};

			// Act
			var response = await Client.PostAsJsonAsync("api/v1/accounts", transferThingsCommand);

			// Assert
			Assert.IsTrue(response.IsSuccessStatusCode);
			Assert.NotNull(response.Headers.Location);
			var transactionOperationId = int.Parse(response.Headers.Location.ToString().Split('/').Last());
			Assert.Greater(transactionOperationId, 0);
			response.Dispose();
		}
	}
}
