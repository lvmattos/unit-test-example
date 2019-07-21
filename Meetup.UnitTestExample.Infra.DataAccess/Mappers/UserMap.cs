using Dapper.FluentMap.Dommel.Mapping;
using Meetup.UnitTestExample.Domain.Model;

namespace Meetup.UnitTestExample.Infra.DataAccess.Mappers
{
    public class UserMap : DommelEntityMap<User>
    {
        public UserMap()
        {
            ToTable("[User]");
            Map(x => x.Id).ToColumn("ID").IsKey();
            Map(x => x.FirstName).ToColumn("FirstName");
            Map(x => x.LastName).ToColumn("LastName");
            Map(x => x.Username).ToColumn("Username");
            Map(x => x.IsBot).ToColumn("IsBot");
        }
    }
}
