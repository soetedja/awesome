﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Awesome.Model;

namespace Awesome.BusinessService.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>> GetCities(int countryId);
    }
}
