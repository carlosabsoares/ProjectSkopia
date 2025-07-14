using AutoMapper;
using Project.Api.Application.Configuration.Mapper;

namespace Project.Api.Api.DependencyMap
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            var config = AutoMapperConfig.RegisterMapper();

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}