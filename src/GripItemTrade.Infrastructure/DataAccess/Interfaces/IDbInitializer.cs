namespace GripItemTrade.Infrastructure.DataAccess.Interfaces
{
	public interface IDbInitializer
	{
		/// <summary>
		/// Creates database and applies migrations.
		/// </summary>
		void Initialize();

		/// <summary>
		/// Adds some default values to the DB.
		/// </summary>
		void SeedData();
	}
}
