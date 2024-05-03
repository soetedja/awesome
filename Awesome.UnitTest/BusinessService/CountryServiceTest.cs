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
    public class CountryServiceTest : IDisposable
    {
        private readonly CountryService _service;
        private readonly MockHelper _mockHelper = new();
        private readonly IServiceProvider _serviceProvider;
        private readonly DataContextTestHelper _dataContextTestHelper;
        public TransactionalTestDatabaseFixture Fixture { get; }

        public CountryServiceTest(TransactionalTestDatabaseFixture fixture)
        {
            Fixture = fixture;
            var fixtureDataContext = Fixture.CreateContext();
            _dataContextTestHelper = new DataContextTestHelper(fixtureDataContext);
            _serviceProvider = ServiceProviderTestHelper.CreateServiceProvider(fixtureDataContext, _mockHelper);
            var dataContext = _serviceProvider.GetService<IDataContext>()!;
            var mapper = _serviceProvider.GetService<IMapper>()!;

            _service = new CountryService(dataContext, mapper);
        }
        public void Dispose() => Fixture.Cleanup();

        [Fact]
        public async Task Get_ReturnsOkObjectResult_WithListOfCountries()
        {
            // Arrange
            _dataContextTestHelper.Add(new Country()
            {
                Name = "Thailand"
            });

            // Act
            var result = await _service.GetCountries();

            // Assert
            Assert.Equal(4, result.Count());
            Assert.Equal("Australia", result.ElementAt(0).Name);
            Assert.Equal("New Zealand", result.ElementAt(1).Name);
            Assert.Equal("Indonesia", result.ElementAt(2).Name);
            Assert.Equal("Thailand", result.ElementAt(3).Name);
        }
    }
}
