using Awesome.Domain;
using Awesome.Repository;
using Microsoft.EntityFrameworkCore;

namespace Awesome.Test.Infrastructures.Db
{
    public class TransactionalTestDatabaseFixture
    {
        public DataContext CreateContext()
            => new DataContext(
                new DbContextOptionsBuilder<DataContext>()
                    .UseSqlServer(new SQLDBConnections().DbConnection)
                    .Options);

        public TransactionalTestDatabaseFixture()
        {
            Cleanup();
        }

        public void Cleanup()
        {
            // Recreate DB for each test method
            using var context = CreateContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            InitializeDataGlobally(context);

        }

        void InitializeDataGlobally(DataContext dataContext)
        {
            // Add the countries & cities
            var countries = new List<Country>()
            {
                new Country() {
                    Name = "Australia",
                    Code = "AU",
                    Cities = new () {
                        new City { Name = "Sydney" },
                        new City { Name = "Melbourne" },
                        new City { Name = "Brisbane" },
                    }
                },
                new Country() {
                    Name = "New Zealand",
                    Code = "NZ",
                    Cities = new () {
                        new City { Name = "Auckland" },
                        new City { Name = "Wellington" },
                        new City { Name = "Christchurch" },
                    }
                },
                new Country() {
                    Name = "Indonesia",
                    Code = "ID",
                    Cities = new () {
                        new City { Name = "Jakarta" },
                        new City { Name = "Yogyakarta" },
                        new City { Name = "Bandung" },
                    }
                }
            };

            dataContext.AddRange(countries);
            dataContext.SaveChanges();
        }
    }
}
