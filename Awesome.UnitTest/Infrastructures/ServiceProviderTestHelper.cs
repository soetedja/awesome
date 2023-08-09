using Awesome.Api;
using Awesome.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Awesome.UnitTest.Infrastructures
{
    public class ServiceProviderTestHelper
    {
        public ServiceProvider CreateServiceProvider(DataContext dataContext, MockHelper mockHelper)
        {
            var serviceCollection = new ServiceCollection();

            //IConfiguration configuration = serviceCollection!.BuildServiceProvider().GetService<IConfiguration>()!;
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {"OpenWeatherMapApiKey", "my-key"}
            });
            var configuration = configBuilder.Build();

            serviceCollection.AddSingleton<IConfiguration>(configuration);

            // Unit Test DI Injenction
            DependencyRegister.RegisterInternalServiceDependencies(serviceCollection);
            RegisterMockedExternalServiceDependencies(serviceCollection, mockHelper);

            serviceCollection.AddDbContext<DataContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(dataContext.Database.GetDbConnection());
            });

            serviceCollection!.AddScoped<IDataContext>(s => s.GetService<DataContext>()!);

            return serviceCollection.BuildServiceProvider();
        }

        public void RegisterMockedExternalServiceDependencies(IServiceCollection serviceCollection, MockHelper mockHelper)
        {
            serviceCollection.AddScoped(sp => mockHelper.HttpClientService().Object);
        }
    }
}
