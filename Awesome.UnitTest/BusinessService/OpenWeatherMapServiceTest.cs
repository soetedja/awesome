using AutoMapper;
using Awesome.BusinessService;
using Awesome.BusinessService.Interfaces;
using Awesome.BusinessService.Utilities;
using Awesome.Model.OpenWeather;
using Awesome.UnitTest.Infrastructures;
using Awesome.UnitTest.Infrastructures.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;

namespace Awesome.UnitTest.BusinessService
{
    [Collection("TransactionalTests")]
    public class OpenWeatherMapServiceTest : IDisposable
    {
        private readonly IOpenWeatherMapService _service;
        private readonly MockHelper _mockHelper = new ();
        private readonly IServiceProvider _serviceProvider;

        public TransactionalTestDatabaseFixture Fixture { get; }

        public OpenWeatherMapServiceTest(TransactionalTestDatabaseFixture fixture)
        {
            Fixture = fixture;
            var fixtureDataContext = Fixture.CreateContext();
            _serviceProvider = new ServiceProviderTestHelper().CreateServiceProvider(fixtureDataContext, _mockHelper);
            
            var configuration = _serviceProvider.GetService<IConfiguration>()!;
            var httpClientService = _serviceProvider.GetService<IHttpClientService>()!;
            var mapper = _serviceProvider.GetService<IMapper>()!;

            _service = new OpenWeatherMapService(configuration, httpClientService, mapper);
        }
        public void Dispose() => Fixture.Cleanup();

        [Fact]
        public async Task GetWeatherByCity_ReturnsOkObjectResult_WithWeatherResponseDto()
        {
            // Arrange
            var jsonString = "{\r\n  \"coord\": {\r\n    \"lon\": -0.1257,\r\n    \"lat\": 51.5085\r\n  },\r\n  \"weather\": [\r\n    {\r\n      \"id\": 804,\r\n      \"main\": \"Clouds\",\r\n      \"description\": \"overcast clouds\",\r\n      \"icon\": \"04n\"\r\n    }\r\n  ],\r\n  \"base\": \"stations\",\r\n  \"main\": {\r\n    \"temp\": 274.38,\r\n    \"feels_like\": 270.66,\r\n    \"temp_min\": 272.2,\r\n    \"temp_max\": 275.5,\r\n    \"pressure\": 1013,\r\n    \"humidity\": 86\r\n  },\r\n  \"visibility\": 10000,\r\n  \"wind\": {\r\n    \"speed\": 3.6,\r\n    \"deg\": 240\r\n  },\r\n  \"clouds\": {\r\n    \"all\": 100\r\n  },\r\n  \"dt\": 1677219227,\r\n  \"sys\": {\r\n    \"type\": 2,\r\n    \"id\": 2075535,\r\n    \"country\": \"GB\",\r\n    \"sunrise\": 1677221845,\r\n    \"sunset\": 1677259830\r\n  },\r\n  \"timezone\": 0,\r\n  \"id\": 2643743,\r\n  \"name\": \"London\",\r\n  \"cod\": 200\r\n}";
            var openWeatherMapResponse = JsonConvert.DeserializeObject<OpenWeatherMapResponse>(jsonString);

            var mockHttpClientService = _mockHelper.HttpClientService();
            mockHttpClientService.Setup(s => s.GetAsync<OpenWeatherMapResponse>(It.IsAny<string>()))!.ReturnsAsync(openWeatherMapResponse);

            // Act
            var result = await _service.GetWeatherByCityNameAsync("London");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("London", result.Location);
            Assert.Equal("24/02/2023 06:13:47", result.Time.ToStringDateTest());
            Assert.Equal(3.6, result.WindSpeed);
            Assert.Equal(240, result.WindDegree);
            Assert.Equal(10000, result.Visibility);
            Assert.Equal("overcast clouds", result.SkyConditions);
            Assert.Equal(274.38, result.TemperatureCelsius);
            Assert.Equal(525.884, result.TemperatureFahrenheit);
            Assert.Equal(271.58, result.DewPoint);
            Assert.Equal(86, result.RelativeHumidity);
            Assert.Equal(1013, result.Pressure);
        }

        [Fact]
        public async Task GetWeatherByCity_ReturnsFailObjectResult()
        {
            // Arrange
            var jsonString = "{\r\n  \"coord\": {\r\n    \"lon\": -0.1257,\r\n    \"lat\": 51.5085\r\n  },\r\n  \"weather\": [\r\n    {\r\n      \"id\": 804,\r\n      \"main\": \"Clouds\",\r\n      \"description\": \"overcast clouds\",\r\n      \"icon\": \"04n\"\r\n    }\r\n  ],\r\n  \"base\": \"stations\",\r\n  \"main\": {\r\n    \"temp\": 274.38,\r\n    \"feels_like\": 270.66,\r\n    \"temp_min\": 272.2,\r\n    \"temp_max\": 275.5,\r\n    \"pressure\": 1013,\r\n    \"humidity\": 86\r\n  },\r\n  \"visibility\": 10000,\r\n  \"wind\": {\r\n    \"speed\": 3.6,\r\n    \"deg\": 240\r\n  },\r\n  \"clouds\": {\r\n    \"all\": 100\r\n  },\r\n  \"dt\": 1677219227,\r\n  \"sys\": {\r\n    \"type\": 2,\r\n    \"id\": 2075535,\r\n    \"country\": \"GB\",\r\n    \"sunrise\": 1677221845,\r\n    \"sunset\": 1677259830\r\n  },\r\n  \"timezone\": 0,\r\n  \"id\": 2643743,\r\n  \"name\": \"London\",\r\n  \"cod\": 200\r\n}";
            var openWeatherMapResponse = JsonConvert.DeserializeObject<OpenWeatherMapResponse>(jsonString);

            var mockHttpClientService = _mockHelper.HttpClientService();
            mockHttpClientService
                .Setup(s => s.GetAsync<OpenWeatherMapResponse>(It.IsAny<string>()))
                .ThrowsAsync(new HttpRequestException());

            // Act
            var ex = await Assert.ThrowsAsync<HttpRequestException>(
                async () => await _service.GetWeatherByCityNameAsync("London")
                );

            // Assert
            Assert.Equal("Exception of type 'System.Net.Http.HttpRequestException' was thrown.", ex.Message);
        }
    }
}
