using Awesome.Api;
using Awesome.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Awesome.Test.Infrastructures
{
    public class ServiceProviderTestHelper
    {
        public static ServiceProvider CreateServiceProvider(DataContext dataContext, MockHelper mockHelper)
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

            // Register App DI
            DependencyRegister.RegisterInternalServiceDependencies(serviceCollection);

            // Register Unit Test DI Injenction (for External Service Call only)
            RegisterMockedExternalServiceDependencies(serviceCollection, mockHelper);

            serviceCollection.AddDbContext<DataContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(dataContext.Database.GetDbConnection());
            });

            serviceCollection!.AddScoped<IDataContext>(s => s.GetService<DataContext>()!);

            return serviceCollection.BuildServiceProvider();
        }

        public static void RegisterMockedExternalServiceDependencies(IServiceCollection serviceCollection, MockHelper mockHelper)
        {
            serviceCollection.AddScoped(sp => mockHelper.HttpClientService().Object);
        }
    }
}
