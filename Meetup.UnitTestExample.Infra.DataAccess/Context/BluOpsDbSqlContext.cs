using System;
using System.Data;

namespace Meetup.UnitTestExample.Infra.DataAccess.Context
{
    internal class DbContext
    {
        public IDbTransaction DbTransaction { get; internal set; }
        public IDbConnection DbConnection { get; internal set; }

        public DbContext()
        {
        }

        public bool SaveChanges()
        {
            try
            {
                if (DbTransaction != null)
                {
                    DbTransaction.Commit();
                }

                return true;
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public bool Rollback()
        {
            try
            {
                if (DbTransaction != null)
                {
                    DbTransaction.Rollback();
                }

                return true;
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public void Dispose()
        {
            if (DbTransaction != null)
            {
                DbTransaction.Dispose();
                DbTransaction = null;
            }
            if (DbConnection != null)
            {
                DbConnection.Dispose();
                DbConnection = null;
            }
            GC.SuppressFinalize(this);
        }
    }
}
