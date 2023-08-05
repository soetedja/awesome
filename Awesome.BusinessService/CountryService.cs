using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Awesome.BusinessService.Interfaces;
using Awesome.Domain;
using Awesome.Model;
using Awesome.Repository;

namespace Awesome.BusinessService
{
    public class CountryService : ICountryService
    {
        private readonly IMapper _mapper;
        private readonly IDataContext _dataContext;

        public CountryService(IDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryDto>> GetCountries()
        {
            var countries = await _dataContext.Countries.ToListAsync();
            return _mapper.Map<IEnumerable<CountryDto>>(countries);
        }
    }
}
