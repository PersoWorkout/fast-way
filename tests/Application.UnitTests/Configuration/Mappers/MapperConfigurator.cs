using Application.Mappers;
using AutoMapper;

namespace Application.UnitTests.Configuration.Mappers
{
    public static class MapperConfigurator
    {
        public static IMapper CreateMapperForUserProfile()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<UserProfile>();
            });

            return configuration.CreateMapper();
        }

        public static IMapper CreateMapperForAuthProfile()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<AuthProfile>();
            });

            return configuration.CreateMapper();
        }
    }
}
