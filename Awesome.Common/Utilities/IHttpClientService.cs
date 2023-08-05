using System.Threading.Tasks;

namespace Awesome.BusinessService.Utilities
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string url);
    }
}
