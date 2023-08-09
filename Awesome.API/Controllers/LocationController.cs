using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Awesome.BusinessService.Interfaces;
using Awesome.Model;

namespace Awesome.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;

        public LocationController(ICountryService countryService, ICityService cityService)
        {
            _countryService = countryService;
            _cityService = cityService;
        }

        [HttpGet("countries")]
        public async Task<IEnumerable<CountryDto>> GetCountry()
        {
            return await _countryService.GetCountries();
        }

        [HttpGet("countries/{code}")]
        public async Task<CountryDto> GetCountryByCode(string code)
        {
            return await _countryService.GetCountryByCode(code);
        }


        [HttpGet("cities/{countryid}")]
        public async Task<IEnumerable<CityDto>> GetCitiesByCountry(int countryId)
        {
            return await _cityService.GetCities(countryId);
        }
    }
}
