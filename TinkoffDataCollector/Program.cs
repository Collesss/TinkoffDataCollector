using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;
using TinkoffDataCollector.Common.Data;
using TinkoffDataCollector.SaveService.Exceptions;
using TinkoffDataCollector.SaveService.Interfaces;
using TinkoffDataCollector.TinkoffDataCollectorService.Exceptions;
using TinkoffDataCollector.TinkoffDataCollectorService.Interfaces;
using TinkoffDataCollector.TinkoffDataService.Exceptions;
using TinkoffDataCollector.TinkoffDataService.Interfaces;

namespace TinkoffDataCollector
{
    internal partial class Program
    {
        static async void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken cancellationToken = cts.Token;

            ITinkoffDataCollectorService tinkoffDataCollectorService = null;
            ILogger<Program> logger = null;

            try
            {
                await tinkoffDataCollectorService.Run(cancellationToken);
            }
            catch(TinkoffDataCollectorServiceException e)
            {
                logger.LogError(e, e.Message);
            }
        }
    }
}