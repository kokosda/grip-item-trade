using System.IO;
using GripItemTrade.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GripItemTrade.Infrastructure.Tests
{
	public abstract class RepositoryBaseTest
	{
		protected readonly IConfigurationRoot configuration;
		protected readonly DbContextOptions<DataContext> dbContextOptions;

		public RepositoryBaseTest()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");

			configuration = builder.Build();
			var connectionString = configuration.GetConnectionString("GripItemTrade.SqlDb");
			dbContextOptions = new DbContextOptionsBuilder<DataContext>().UseSqlServer(connectionString).Options;
		}
	}
}
