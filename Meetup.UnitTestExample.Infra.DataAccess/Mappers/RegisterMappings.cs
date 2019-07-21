using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;

namespace Meetup.UnitTestExample.Infra.DataAccess.Mappers
{
    public class RegisterMappings
    {
        public static void Register()
        {
            if (FluentMapper.EntityMaps.TryAdd(typeof(UserMap), new UserMap()))
            {
                FluentMapper.Initialize(config =>
                {
                    config.AddMap(new UserMap());
                    config.ForDommel();
                });
            }
        }
    }
}
