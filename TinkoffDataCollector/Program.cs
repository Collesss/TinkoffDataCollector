using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TinkoffDataCollectorService.Exceptions;
using TinkoffDataCollectorService.Interfaces;

namespace TinkoffDataCollector
{
    internal partial class Program
    {
        private static IServiceProvider ServiceProvider { get; set; }
        private static IConfiguration Configuration { get; set; }

        static async Task Main(string[] args)
        {
            Init(args);

            CancellationTokenSource cts = new();
            CancellationToken cancellationToken = cts.Token;

            using IServiceScope serviceScope = ServiceProvider.CreateScope();
            IServiceProvider scopeServiceProvider = serviceScope.ServiceProvider;

            ITinkoffDataCollectorService tinkoffDataCollectorService = scopeServiceProvider.GetRequiredService<ITinkoffDataCollectorService>();
            ILogger<Program> logger = scopeServiceProvider.GetRequiredService<ILogger<Program>>();

            //Console.CancelKeyPress += (e, sender) => cts.Cancel();

            try
            {
                //Console.WriteLine("test1");
                //await Task.Delay(2500, cancellationToken);
                //Console.WriteLine("test2");
                await tinkoffDataCollectorService.Run(cancellationToken);
            }
            catch (TinkoffDataCollectorServiceException e)
            {
                switch (e.InnerException)
                {
                    case OperationCanceledException:
                        //Console.WriteLine("Canceled.");
                        logger.LogWarning("The operation was canceled.");
                        break;
                    default:
                        //Console.WriteLine("Error");
                        logger.LogError(e, e.Message);
                        break;
                }
            }
        }
    }
}