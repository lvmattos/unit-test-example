using Meetup.UnitTestExample.Infra.DataAccess.Mappers;
using Meetup.UnitTestExample.Infra.DataAccess.UoW;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Data.SQLite;
using System.IO;

namespace Meetup.UnitTestExample.Test.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        public UnitOfWork UnitOfWork;
        private ConnectionFactorySQLite _connectionFactory;
        private Mock<IConfiguration> _configuration;

        public DatabaseFixture()
        {
            Mock<IConfigurationSection> configurationSectionStub = new Mock<IConfigurationSection>();
            configurationSectionStub.Setup(x => x["DefaultConnection"])
                 .Returns("Data Source=:memory:;Version=3;New=True;");

            _configuration = new Mock<IConfiguration>();
            _configuration.Setup(x => x.GetSection("ConnectionStrings"))
                .Returns(configurationSectionStub.Object);
            _connectionFactory = new ConnectionFactorySQLite(_configuration.Object);

            UnitOfWork = new UnitOfWork(_connectionFactory);

            RegisterMappings.Register();
        }

        public void OpenConnection()
        {
            if (UnitOfWork != null && UnitOfWork.Connection() == null)
            {
                UnitOfWork.OpenConnection();
                SQLiteConnection sQLiteConnection = (SQLiteConnection)UnitOfWork.Connection();

                SQLiteCommand command = new SQLiteCommand(GetDataBaseModel(), sQLiteConnection);
                command.ExecuteNonQuery();
            }
        }

        private string GetDataBaseModel()
        {
            return File.ReadAllText("ScriptDatabase\\CreateDataBase-SQLite.sql");
        }

        public void Dispose()
        {
            UnitOfWork = null;
        }
    }
}
