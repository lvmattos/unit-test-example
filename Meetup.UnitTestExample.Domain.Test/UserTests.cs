using Meetup.UnitTestExample.Domain.Model;
using Meetup.UnitTestExample.Domain.Repository;
using Meetup.UnitTestExample.Domain.Service;
using Meetup.UnitTestExample.Test;
using Meetup.UnitTestExample.Test.Fixtures;
using Moq;
using Xunit;

namespace Meetup.UnitTestExample.Domain.Test
{
    public class UserTests : TestBase
    {
        private Mock<IUserRepository> _userRepository;

        public UserTests(DatabaseFixture databaseFixture) :
            base(databaseFixture)
        {
            _userRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public void Should_ToString()
        {
            Assert.Equal("@alicebot (12345)", new User(12345)
            {
                IsBot = true,
                FirstName = "Alice Bot",
                Username = "alicebot"
            }.ToString());

            Assert.Equal("BoBBy (67890)", new User(67890)
            {
                IsBot = false,
                FirstName = "BoBBy"
            }.ToString());

            Assert.Equal("Chris Dale (54321)", new User(54321)
            {
                IsBot = false,
                FirstName = "Chris",
                LastName = "Dale"
            }.ToString());
        }

        [Fact]
        public void Should_Name_Required()
        {
            UserService userService = new UserService(DatabaseFixture.UnitOfWork,
                _userRepository.Object);

            userService.RegisterNew(new User());

            Assert.Contains(userService.Notifications, s => s.Key == "UserError1");
            _userRepository.Verify(x => x.Insert(It.IsAny<User>()), Times.Never);
        }
    }
}
