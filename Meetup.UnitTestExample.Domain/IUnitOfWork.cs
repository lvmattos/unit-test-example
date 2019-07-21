using System;
using System.Data;

namespace Meetup.UnitTestExample.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        void OpenConnection();
        IDbConnection Connection();
        IDbTransaction Transaction();
        void BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted);
        bool Commit();
        bool Rollback();
    }
}
