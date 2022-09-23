using Microsoft.Extensions.Logging;
using TinkoffDataCollector.Common.Data;
using TinkoffDataCollector.TinkoffDataService.Interfaces;

namespace TinkoffDataCollector.TinkoffDataService.Implementation.GetDataCacheHttpClient
{
    public class TinkoffDataService : ITinkoffDataService
    {
        private readonly ILogger<TinkoffDataService> _logger;

        public TinkoffDataService(ILogger<TinkoffDataService> logger)
        {
            _logger = logger;
        }

        public Task<IEnumerable<MarketInstrument>> GetAllStoks(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CandlePayload>> GetStockCandles(string figi, DateTime from, DateTime to, CandleInterval interval, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
