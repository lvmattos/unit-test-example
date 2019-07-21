using System.Data;

namespace Meetup.UnitTestExample.Infra.DataAccess.Repository.Connection
{
    public interface IConnectionFactory
    {
        IDbConnection OpenConnection();
    }
}
