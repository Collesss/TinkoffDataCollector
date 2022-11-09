using Common.Data;

namespace DataService.GetDataCacheHttpClient.HttpClient.Interfaces
{
    public interface IHttpClient
    {
        Task<IEnumerable<MarketInstrument>> GetStoks(CancellationToken cancellationToken);

        Task<IEnumerable<CandlePayload>> GetCandles(string figi, DateTime from, DateTime to, CancellationToken cancellationToken);
    }
}
