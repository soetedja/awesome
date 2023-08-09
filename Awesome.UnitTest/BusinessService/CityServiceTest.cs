using AutoMapper;
using Awesome.BusinessService;
using Awesome.Domain;
using Awesome.Repository;
using Awesome.UnitTest.Infrastructures;
using Awesome.UnitTest.Infrastructures.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Awesome.UnitTest.BusinessService
{
    [Collection("TransactionalTests")]
    public class CityServiceTest : IDisposable
    {
        private readonly CityService _service;
        private readonly IServiceProvider _serviceProvider;
        private readonly DataContextTestHelper _dataContextHelper;

        private readonly MockHelper _mockHelper = new();
        public TransactionalTestDatabaseFixture Fixture { get; }

        public CityServiceTest(TransactionalTestDatabaseFixture fixture)
        {
            Fixture = fixture;
            var dataContext = Fixture.CreateContext();
            _dataContextHelper = new DataContextTestHelper(dataContext);
            _serviceProvider = new ServiceProviderTestHelper().CreateServiceProvider(dataContext, _mockHelper);
            var mapper = _serviceProvider.GetService<IMapper>()!;
            _service = new CityService(dataContext, mapper);
        }

        public void Dispose() => Fixture.Cleanup();

        [Fact]
        public async Task Get_ReturnsOkObjectResult_WithListOfCities()
        {
            // Arrange
            var dataContext = _serviceProvider.GetService<IDataContext>()!;

            var country = dataContext.Countries.FirstOrDefault(s => s.Code == "ID")!;
            _dataContextHelper.AddRange(new List<City>() {
                new City()
                {
                    Name = "Solo",
                    CountryId = country.Id
                },
                new City()
                {
                    Name = "Surabaya",
                    CountryId = country.Id
                }
            });

            // Act
            var result = await _service.GetCities(country.Id);

            // Assert
            Assert.Equal(5, result.Count());
            Assert.Equal("Jakarta", result.ElementAt(0).Name);
            Assert.Equal("Yogyakarta", result.ElementAt(1).Name);
            Assert.Equal("Bandung", result.ElementAt(2).Name);
            Assert.Equal("Solo", result.ElementAt(3).Name);
            Assert.Equal("Surabaya", result.ElementAt(4).Name);
        }
    }
}
