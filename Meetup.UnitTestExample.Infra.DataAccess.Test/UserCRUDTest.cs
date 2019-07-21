using Meetup.UnitTestExample.Domain.Model;
using Meetup.UnitTestExample.Infra.DataAccess.Repository;
using Meetup.UnitTestExample.Test;
using Meetup.UnitTestExample.Test.Attributes;
using Meetup.UnitTestExample.Test.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Meetup.UnitTestExample.Infra.DataAccess.Test
{
    public class UserCRUDTest : TestBase
    {
        private readonly UserRepository _userRepository;

        public UserCRUDTest(DatabaseFixture databaseFixture)
            : base(databaseFixture)
        {
            _userRepository = new UserRepository(databaseFixture.UnitOfWork);
            OpenConnection();
        }

        [Theory, Order(1)]
        [InlineData("Chris", "Dale", "chrisDale", true)]
        public void Should_Insert(string firstName, 
            string lastName, string username, bool isBot)
        {
            try
            {
                User user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Username = username,
                    IsBot = isBot
                };

                Task<object> insertTask = _userRepository.Insert(user);
                insertTask.Wait();
                DatabaseFixture.UnitOfWork.Commit();

                Assert.True(((long)insertTask.Result) != 0);
            }
            catch (Exception e)
            {
                AssertException(e);
            }
        }

        [Fact, Order(2)]
        public void Should_Get_One()
        {
            try
            {
                Task<IEnumerable<User>> task = _userRepository.GetAll();
                Assert.True(task.Result.Any());
            }
            catch (Exception e)
            {
                AssertException(e);
            }
        }
    }
}
