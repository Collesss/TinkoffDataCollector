using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TinkoffDataCollector
{
    partial class Program
    {
        static void Init(string[] args)
        {

            Configuration = new ConfigurationBuilder()
                .AddJsonFile($@"{Directory.GetCurrentDirectory()}\appsetting.json", true)
                .AddCommandLine(args)
                .Build();

            IServiceCollection serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
