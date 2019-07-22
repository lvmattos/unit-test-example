using Meetup.UnitTestExample.Domain.Service;
using Meetup.UnitTestExample.Test.Attributes;
using System;
using Xunit;

namespace Meetup.UnitTestExample.Domain.Test
{
    public class BotIdTests
    {
        public BotIdTests()
        {

        }

        [Fact]
        public void Should_Throw_On_Null_Token()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new BotClient(null)
            );
            Assert.Equal("token", exception.ParamName);
        }

        [Theory]
        [InlineData("1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy", 1234567)]
        [InlineData("9:jdsaghdfilghdfiugherh", 9)]
        [InlineData("0:foo", 0)]
        [InlineData("5:", 5)]
        [InlineData("-123::::", -123)]
        public void Should_Parse_Bot_Id(string token, int expectedId)
        {
            var botClient = new BotClient(token);
            Assert.Equal(expectedId, botClient.BotId);
        }

        [Theory]
        [JsonFileData("JsonData\\BotIdTests.json", 
            "Should_Throw_On_Invalid_Token")]
        public void Should_Throw_On_Invalid_Token(string invalidToken)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new BotClient(invalidToken)
            );
            Assert.Equal("token", exception.ParamName);
        }
    }
}
