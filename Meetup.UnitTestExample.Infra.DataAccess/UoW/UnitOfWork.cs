using Meetup.UnitTestExample.Domain;
using Meetup.UnitTestExample.Infra.DataAccess.Context;
using Meetup.UnitTestExample.Infra.DataAccess.Repository.Connection;
using System.Data;

namespace Meetup.UnitTestExample.Infra.DataAccess.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly IConnectionFactory _connectionFactory;

        public UnitOfWork(IConnectionFactory connectionFactory)
        {
            _dbContext = new DbContext();
            _connectionFactory = connectionFactory;
        }

        public IDbConnection Connection()
        {
            return _dbContext.DbConnection;
        }

        public IDbTransaction Transaction()
        {
            return _dbContext.DbTransaction;
        }

        public void BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            if (_dbContext.DbTransaction == null ||
                (_dbContext.DbTransaction != null &&
                _dbContext.DbTransaction.Connection == null))
            {
                _dbContext.DbTransaction =
                _dbContext.DbConnection.BeginTransaction(level);
            }
        }

        public void OpenConnection()
        {
            _dbContext.DbConnection = _connectionFactory.OpenConnection();
            _dbContext.DbConnection.Open();
        }

        public bool Commit()
        {
            return _dbContext.SaveChanges();
        }

        public bool Rollback()
        {
            return _dbContext.Rollback();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
