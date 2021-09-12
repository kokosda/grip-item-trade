using GripItemTrade.Infrastructure.DataAccess.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GripItemTrade.Infrastructure.DataAccess.Extensions
{
	public static class ApplicationDatabaseInitializer
	{
		public static IApplicationBuilder UseDatabaseInitializer(this IApplicationBuilder app)
		{
			var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
			using (var scope = scopeFactory.CreateScope())
			{
				var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
				dbInitializer.Initialize();
				dbInitializer.SeedData();
			}

			return app;
		}
	}
}
