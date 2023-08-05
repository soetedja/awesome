using System.Threading.Tasks;
using Awesome.Model;

namespace Awesome.BusinessService.Interfaces
{
    public interface IOpenWeatherMapService
    {
        Task<WeatherResponseDto> GetWeatherByCityNameAsync(string cityName);
    }
}
