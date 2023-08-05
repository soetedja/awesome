using AutoMapper;
using Microsoft.Extensions.Configuration;
using Awesome.BusinessService.Interfaces;
using Awesome.BusinessService.Utilities;
using Awesome.Model;
using Awesome.Model.OpenWeather;

namespace Awesome.BusinessService
{
    public class OpenWeatherMapService : IOpenWeatherMapService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _apiKey;
        private readonly IMapper _mapper;

        public OpenWeatherMapService(IConfiguration configuration, IHttpClientService httpClientService, IMapper mapper)
        {
            _httpClientService = httpClientService;
            _apiKey = configuration.GetValue<string>("OpenWeatherMapApiKey") ?? "";
            _mapper = mapper;
        }

        public async Task<WeatherResponseDto> GetWeatherByCityNameAsync(string cityName)
        {
            try
            {
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={_apiKey}";

                var response = await _httpClientService.GetAsync<OpenWeatherMapResponse>(url);

                return _mapper.Map<WeatherResponseDto>(response);
            }
            catch
            {
                throw;
            }
        }
    }
}
