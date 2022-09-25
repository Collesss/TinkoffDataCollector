using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TinkoffDataCollector.TinkoffDataCollectorService.Exceptions;
using TinkoffDataCollector.TinkoffDataCollectorService.Interfaces;

namespace TinkoffDataCollector
{
    internal partial class Program
    {
        private static IServiceProvider ServiceProvider { get; set; }
        private static IConfiguration Configuration { get; set; }

        static async void Main(string[] args)
        {
            Init(args);

            CancellationTokenSource cts = new();
            CancellationToken cancellationToken = cts.Token;

            using IServiceScope serviceScope = ServiceProvider.CreateScope();
            IServiceProvider scopeServiceProvider = serviceScope.ServiceProvider;

            ITinkoffDataCollectorService tinkoffDataCollectorService = scopeServiceProvider.GetRequiredService<ITinkoffDataCollectorService>();
            ILogger<Program> logger = scopeServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                await tinkoffDataCollectorService.Run(cancellationToken);
            }
            catch (TinkoffDataCollectorServiceException e)
            {
                logger.LogError(e, e.Message);
            }
        }
    }
}