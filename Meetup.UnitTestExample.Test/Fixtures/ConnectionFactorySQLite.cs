using Meetup.UnitTestExample.Infra.DataAccess.Repository.Connection;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace Meetup.UnitTestExample.Test.Fixtures
{
    public class ConnectionFactorySQLite : IConnectionFactory
    {
        private string ConnectionString { get; set; }

        public ConnectionFactorySQLite(IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("DefaultConnection");
        }

        public IDbConnection OpenConnection()
        {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient",
                System.Data.SQLite.SQLiteFactory.Instance);

            DbProviderFactory factory =
                DbProviderFactories.GetFactory("System.Data.SqlClient");

            DbConnection dbConnection = factory.CreateConnection();
            dbConnection.ConnectionString = ConnectionString;

            return dbConnection;
        }
    }
}
