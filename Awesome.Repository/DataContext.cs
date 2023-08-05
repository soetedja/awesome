using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Awesome.Domain;

namespace Awesome.Repository
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppSetting> AppSettings { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<City> Cities { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite("Data Source=mydatabase.db");

        public void InitializeData()
        {
            // Add the countries
            var countries = new List<Country>
            {
                new Country { Name = "Indonesia" ,  Code = "ID"},
                new Country { Name = "Australia",  Code = "AU" },
                new Country { Name = "United States",  Code = "US" },
                new Country { Name = "Canada",  Code = "CA" }
            };

            Countries.AddRange(countries);
            SaveChanges();

            // Add the cities
            var cities = new List<City>
            {
                new City { Name = "Jakarta", CountryId = countries[0].Id },
                new City { Name = "Yogyakarta", CountryId = countries[0].Id },
                new City { Name = "Bandung", CountryId = countries[0].Id },
                new City { Name = "Sydney", CountryId = countries[1].Id },
                new City { Name = "Melbourne", CountryId = countries[1].Id },
                new City { Name = "Brisbane", CountryId = countries[1].Id },
                new City { Name = "New York", CountryId = countries[2].Id },
                new City { Name = "Los Angeles", CountryId = countries[2].Id },
                new City { Name = "Chicago", CountryId = countries[2].Id },
                new City { Name = "Toronto", CountryId = countries[3].Id },
                new City { Name = "Montreal", CountryId = countries[3].Id },
                new City { Name = "Vancouver", CountryId = countries[3].Id }
            };
            Cities.AddRange(cities);
            SaveChanges();

            var appSettings = new List<AppSetting>
            {
                new AppSetting("DataInitialized", "false")
            };

            AppSettings.AddRange(appSettings);
            SaveChanges();
        }

    }
}
