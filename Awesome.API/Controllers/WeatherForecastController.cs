using Awesome.BusinessService.Interfaces;
using Awesome.Model;
using Microsoft.AspNetCore.Mvc;

namespace Awesome.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IOpenWeatherMapService _openWeatherMapService;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(IOpenWeatherMapService openWeatherMapService)
        {
            _openWeatherMapService = openWeatherMapService;
        }

        [HttpGet("city/{cityName}")]
        public async Task<WeatherResponseDto> GetWeatherByCity(string cityName)
        {
            return await _openWeatherMapService.GetWeatherByCityNameAsync(cityName);
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}