using Microsoft.EntityFrameworkCore;
using Awesome.Domain;

namespace Awesome.Repository
{
    public interface IDataContext
    {
        void InitializeData();
        DbSet<AppSetting> AppSettings { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<City> Cities { get; set; }
    }
}
