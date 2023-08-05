using AutoMapper;

namespace Awesome.BusinessService.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
    }
}
