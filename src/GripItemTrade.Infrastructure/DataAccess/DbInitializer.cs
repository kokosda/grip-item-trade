using GripItemTrade.Domain.Accounts;
using GripItemTrade.Domain.Customers;
using GripItemTrade.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GripItemTrade.Infrastructure.DataAccess
{
	public class DbInitializer : IDbInitializer
	{
		private readonly IServiceScopeFactory serviceScopeFactory;
		private readonly Random random;

		public DbInitializer(IServiceScopeFactory serviceScopeFactory)
		{
			this.serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
			random = new Random();
		}

		public void Initialize()
		{
			using (var serviceScope = serviceScopeFactory.CreateScope())
			using (var context = serviceScope.ServiceProvider.GetService<DataContext>())
				context.Database.Migrate();
		}

		public void SeedData()
		{
			using (var serviceScope = serviceScopeFactory.CreateScope())
			using (var context = serviceScope.ServiceProvider.GetService<DataContext>())
			{
				SeedCustomers(context);
				context.SaveChanges();
			}
		}

		private void SeedCustomers(DataContext dataContext)
		{
			if (dataContext.Customers.Any())
				return;

			var customers = new[]
			{
				new Customer { FirstName = "Alice", LastName = "Smith" },
				new Customer { FirstName = "Bob", LastName = "Laserson" }
			};

			SeedAccounts(dataContext, customers);
			dataContext.Customers.AddRange(customers);

		}

		private void SeedAccounts(DataContext dataContext, ICollection<Customer> customers)
		{
			if (dataContext.Accounts.Any())
				return;

			var accounts = new List<Account>();

			foreach (var customer in customers)
			{
				accounts.Add(new Account
				{
					Customer = customer
				});
			}

			SeedBalanceEntries(dataContext, accounts);
			dataContext.Accounts.AddRange(accounts);
		}

		private void SeedBalanceEntries(DataContext dataContext, ICollection<Account> accounts)
		{
			if (dataContext.BalanceEntry.Any())
				return;

			foreach (var account in accounts)
			{
				account.BalanceEntries.AddRange(new[]
				{
					new BalanceEntry
					{
						Account = account,
						Amount = (decimal)random.Next(20, 50),
						Code = "BROOM",
						Customer = account.Customer
					},
					new BalanceEntry
					{
						Account = account,
						Amount = (decimal)random.Next(20, 50),
						Code = "STICK",
						Customer = account.Customer
					},
					new BalanceEntry
					{
						Account = account,
						Amount = (decimal)random.Next(20, 50),
						Code = "FLOWER",
						Customer = account.Customer
					}
				});
			}
		}
	}
}
