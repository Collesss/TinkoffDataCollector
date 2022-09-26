using Common.Data;

namespace DataService.GetDataCacheHttpClient.HttpClient.Interfaces
{
    public interface IHttpClient
    {
        IEnumerable<MarketInstrument> GetStoks(CancellationToken cancellationToken);

        IEnumerable<CandlePayload> GetCandles(string figi, DateTime from, DateTime to, CancellationToken cancellationToken);
    }
}
