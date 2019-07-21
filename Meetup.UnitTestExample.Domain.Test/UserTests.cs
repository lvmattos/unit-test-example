using Meetup.UnitTestExample.Domain.Model;
using Xunit;

namespace Meetup.UnitTestExample.Domain.Test
{
    public class UserTests
    {
        public UserTests()
        {

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
    }
}
