using TinkoffDataCollector.Common.Data;
using TinkoffDataCollector.TinkoffDataService.Interfaces;

namespace TinkoffDataCollector.TinkoffDataService.Implementation.GetDataCacheHttpClient
{
    public class TinkoffDataService : ITinkoffDataService
    {
        public Task<IEnumerable<MarketInstrument>> GetAllMarketInstrument(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CandlePayload>> GetMarketCandle(string figi, DateTime from, DateTime to, CandleInterval interval, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
