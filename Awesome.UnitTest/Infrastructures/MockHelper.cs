using Awesome.BusinessService.Utilities;
using Moq;

namespace Awesome.Test.Infrastructures
{
    public class MockHelper
    {
        private readonly Mock<IHttpClientService> mockHttpClientService = new();

        public MockHelper CreateMock()
             => new();

        public Mock<IHttpClientService> HttpClientService()
        {
            return mockHttpClientService;
        }
    }
}
