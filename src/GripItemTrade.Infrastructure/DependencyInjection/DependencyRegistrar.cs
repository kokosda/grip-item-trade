﻿using Microsoft.Extensions.DependencyInjection;
using GripItemTrade.Core.Interfaces;
using GripItemTrade.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GripItemTrade.Infrastructure.DependencyInjection
{
	public static class DependencyRegistrar
	{
		public static IServiceCollection AddInfrastructureLevelServices(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("GripItemTrade.SqlDb");

			serviceCollection.AddDbContext<DataContext>(options =>
			{
				options.UseSqlServer(connectionString, a => a.MigrationsAssembly("GripItemTrade.Api"));
			});

			serviceCollection.AddSingleton(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
			return serviceCollection;
		}
	}
}
