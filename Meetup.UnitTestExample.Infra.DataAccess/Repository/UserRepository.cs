using Meetup.UnitTestExample.Domain;
using Meetup.UnitTestExample.Domain.Model;
using Meetup.UnitTestExample.Domain.Repository;
using Meetup.UnitTestExample.Infra.DataAccess.Repository.Base;

namespace Meetup.UnitTestExample.Infra.DataAccess.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
