using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TinkoffDataCollector.Common.Data;
using TinkoffDataCollector.SaveService.Exceptions;
using TinkoffDataCollector.SaveService.Interfaces;
using TinkoffDataCollector.TinkoffDataService.Exceptions;
using TinkoffDataCollector.TinkoffDataService.Interfaces;

namespace TinkoffDataCollector
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            ILogger<Program> logger = null;
            ISaveService saveService = null;
            ITinkoffDataService tinkoffDataService = null;
            IStringLocalizer stringLocalizer = null;

            CancellationTokenSource cancellationTokenSource = new();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            try
            {
                foreach (var stock in await tinkoffDataService.GetAllStoks(cancellationToken))
                {
                    IEnumerable<CandlePayload> candles;

                    try
                    {
                        candles = await tinkoffDataService.GetStockCandles(stock.Figi, DateTime.Now.AddDays(-5), DateTime.Now, CandleInterval.Hour, cancellationToken);
                    }
                    catch (TinkoffDataServiceException e)
                    {
                        logger.LogError(e, stringLocalizer["Programm.TinkoffDataServiceException:ErrorLoadCandles", e.Message]);
                        continue;
                    }

                    try
                    {
                        await saveService.Save(stock, candles, cancellationToken);
                    }
                    catch (SaveServiceException e)
                    {
                        logger.LogError(e, stringLocalizer["Programm.SaveServiceException", e.Message]);
                        continue;
                    }
                }
            }
            catch (TinkoffDataServiceException e)
            {
                logger.LogError(e, stringLocalizer["Programm.TinkoffDataServiceException:ErrorLoadStoks", e.Message]);
            }
            catch(OperationCanceledException e)
            {
                logger.LogError(e, stringLocalizer["Programm.OperationCanceledException"]);
            }
        }
    }
}