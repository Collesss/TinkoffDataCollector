using System;
using System.Collections.Generic;
using System.Threading;
using TinkoffDataCollector.Common.Data;

namespace TinkoffDataCollector.TinkoffDataService.Implementation.GetDataCacheHttpClient.TinkoffHttpClient.Interfaces
{
    public interface ITinkoffHttpClient
    {
        IEnumerable<MarketInstrument> GetStoks(CancellationToken cancellationToken);

        IEnumerable<CandlePayload> GetCandles(string figi, DateTime from, DateTime to, CancellationToken cancellationToken);
    }
}
