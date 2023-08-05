using System.Collections.Generic;
using System.Threading.Tasks;
using Awesome.Model;

namespace Awesome.BusinessService.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDto>> GetCountries();
    }
}
