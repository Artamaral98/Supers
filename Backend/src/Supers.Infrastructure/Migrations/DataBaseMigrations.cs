using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace Supers.Infrastructure.Migrations
{
    public static class DataBaseMigrations
    {
        public static void Migrate(string connectionString, IServiceProvider serviceProvider)
        {
            EnsureDataBaseCreated(connectionString);
            MigrationDatabase(serviceProvider);
        }

        private static void EnsureDataBaseCreated(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = connectionStringBuilder.InitialCatalog; 

            connectionStringBuilder.Remove("Database");

            var parameters = new DynamicParameters();
            parameters.Add("name", databaseName);

            using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString);

            var records = dbConnection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);

            if (records.Any() == false) 
            {
                dbConnection.Execute($"CREATE DATABASE {databaseName}");
            }
        }

        private static void MigrationDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>(); 
            runner.ListMigrations(); 
            runner.MigrateUp(); 
        }

    }
}
