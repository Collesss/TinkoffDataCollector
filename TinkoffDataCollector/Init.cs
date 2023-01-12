using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TinkoffDataCollector
{
    partial class Program
    {
        static void Init(string[] args)
        {
            //{Directory.GetCurrentDirectory()}
            Configuration = new ConfigurationBuilder()
                .AddJsonFile($@"{Directory.GetCurrentDirectory()}\appsettings.json", true)
                .AddCommandLine(args)
                .Build();

            IServiceCollection serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
