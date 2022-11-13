using DataService.GetDataCacheHttpClient.HttpClient.Data;

namespace DataService.GetDataCacheHttpClient.HttpClient.Interfaces
{
    public interface IHttpClientTinkoff
    {
        Task<MarketInstrumentList> GetStoks(CancellationToken cancellationToken = default);

        Task<CandleList> GetCandles(string figi, DateTime from, DateTime to, CandleInterval interval, CancellationToken cancellationToken = default);
    }
}
