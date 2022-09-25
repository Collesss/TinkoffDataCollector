using Microsoft.Extensions.DependencyInjection;

namespace TinkoffDataCollector
{
    internal partial class Program
    {
        static void ConfigureServices(IServiceCollection serviceProvider)
        {
            serviceProvider
                .AddSingleton(Configuration);
        }
    }
}
