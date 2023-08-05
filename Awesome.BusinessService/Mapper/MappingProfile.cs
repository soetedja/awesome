using AutoMapper;
using Awesome.Domain;
using Awesome.Model;
using Awesome.Model.OpenWeather;

namespace Awesome.BusinessService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entities Mapper
            CreateMap<Country, CountryDto>();
            CreateMap<City, CityDto>();

            // External Entities Mapper
            CreateMap<OpenWeatherMapResponse, WeatherResponseDto>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.Dt).DateTime))
                .ForMember(dest => dest.WindSpeed, opt => opt.MapFrom(src => src.Wind.Speed))
                .ForMember(dest => dest.WindDegree, opt => opt.MapFrom(src => src.Wind.Deg))
                .ForMember(dest => dest.Visibility, opt => opt.MapFrom(src => src.Visibility))
                .ForMember(dest => dest.SkyConditions, opt => opt.MapFrom(src => src.Weather[0].Description))
                .ForMember(dest => dest.TemperatureCelsius, opt => opt.MapFrom(src => src.Main.Temp))
                .ForMember(dest => dest.TemperatureFahrenheit, opt => opt.MapFrom(src => src.Main.Temp * 1.8 + 32))
                .ForMember(dest => dest.DewPoint, opt => opt.MapFrom(src => src.Main.Temp - ((100 - src.Main.Humidity) / 5.0)))
                .ForMember(dest => dest.RelativeHumidity, opt => opt.MapFrom(src => src.Main.Humidity))
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.Main.Pressure));
        }
    }
}
