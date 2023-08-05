using AutoMapper;
using Awesome.BusinessService.Interfaces;
using Awesome.Model;
using Awesome.Repository;
using Microsoft.EntityFrameworkCore;

namespace Awesome.BusinessService
{
    public class CityService : ICityService
    {

        private readonly IMapper _mapper;
        private readonly IDataContext _dataContext;

        public CityService(IDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityDto>> GetCities(int countryId)
        {
            var cities = await _dataContext.Cities
                .Include(i => i.Country)
                .Where(s => s.CountryId == countryId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CityDto>>(cities);
        }
    }
}
