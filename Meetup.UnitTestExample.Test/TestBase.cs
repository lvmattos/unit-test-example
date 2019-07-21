using Meetup.UnitTestExample.Test.Attributes;
using Meetup.UnitTestExample.Test.Fixtures;
using System;
using Xunit;

namespace Meetup.UnitTestExample.Test
{
    [TestCaseOrderer(
    CustomTestCaseOrderer.TypeName,
    CustomTestCaseOrderer.AssembyName)]
    public class TestBase : IClassFixture<DatabaseFixture>
    {
        protected static int Order;
        protected readonly DatabaseFixture DatabaseFixture;

        public TestBase(DatabaseFixture databaseFixture)
        {
            this.DatabaseFixture = databaseFixture;
        }

        public void OpenConnection()
        {
            DatabaseFixture.OpenConnection();
        }

        protected void AssertException(Exception e)
        {
            Assert.True(false, e.Message);
        }
    }
}
