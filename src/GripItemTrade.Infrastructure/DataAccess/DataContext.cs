using GripItemTrade.Domain.Accounts;
using GripItemTrade.Domain.Customers;
using GripItemTrade.Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using System;

namespace GripItemTrade.Infrastructure.DataAccess
{
	public class DataContext : DbContext
	{
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<BalanceEntry> BalanceEntries { get; set; }
		public DbSet<TransactionalOperation> TransactionalOperations { get; set; }
		public DbSet<TransactionalOperationEntry> TransactionOperationEntries { get; set; }

		public DataContext(DbContextOptions contextOptions) : base(contextOptions)
		{
		}
	}
}
