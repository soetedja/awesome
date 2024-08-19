using Awesome.Api.Controllers;
using Awesome.BusinessService.Interfaces;
using Awesome.Domain;
using Awesome.Test.Infrastructures;
using Awesome.Test.Infrastructures.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Awesome.Test.Api
{
    [Collection("TransactionalTests")]
    public class LocationControllerTest : IDisposable
    {

        private readonly LocationController _controller;
        private readonly IServiceProvider _serviceProvider;
        private readonly DataContextTestHelper _dataContextTestHelper;
        private readonly MockHelper _mockHelper = new();
        private TransactionalTestDatabaseFixture Fixture { get; }

        public LocationControllerTest(TransactionalTestDatabaseFixture fixture)
        {
            Fixture = fixture;
            var dataContext = Fixture.CreateContext();
            _dataContextTestHelper = new DataContextTestHelper(dataContext);

            _serviceProvider = ServiceProviderTestHelper.CreateServiceProvider(dataContext, _mockHelper);
            var countryService = _serviceProvider.GetService<ICountryService>()!;
            var cityService = _serviceProvider.GetService<ICityService>()!;

            _controller = new LocationController(countryService, cityService);
        }


        [Fact]
        public async Task Get_ReturnsOkObjectResult_WithListOfCountries2()
        {
            // Arrange
            _dataContextTestHelper.Add(new Country()
            {
                Name = "Singapore2"
            });

            // Act
            var result = await _controller.GetCountry();

            // Assert
            Assert.Equal(4, result.Count());
            Assert.Equal("Australia", result.ElementAt(0).Name);
            Assert.Equal("New Zealand", result.ElementAt(1).Name);
            Assert.Equal("Indonesia", result.ElementAt(2).Name);
            Assert.Equal("Singapore2", result.ElementAt(3).Name);
        }

        [Fact]
        public async Task Get_ReturnsOkObjectResult_WithListOfCountries()
        {
            // Arrange

            // Act
            var result = await _controller.GetCountry();

            // Assert
            Assert.Equal(3, result.Count());
            Assert.Equal("Australia", result.ElementAt(0).Name);
            Assert.Equal("New Zealand", result.ElementAt(1).Name);
            Assert.Equal("Indonesia", result.ElementAt(2).Name);
        }

        [Fact]
        public async Task Add_Country_ReturnsOkObjectResult()
        {
            // Arrange
            _dataContextTestHelper.Add(new Country()
            {
                Name = "Singapore",
                Code = "SG"
            });

            // Act
            var result = await _controller.GetCountryByCode("SG");

            // Assert
            Assert.Equal("Singapore", result.Name);
        }

        public void Dispose() => Fixture.Cleanup();
    }
}
