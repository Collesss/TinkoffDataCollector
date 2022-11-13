using DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data;

namespace DataService.GetDataCacheHttpClient.HttpClientTinkoff.Interfaces
{
    public interface IHttpClientTinkoff
    {
        Task<MarketInstrumentList> GetStoks(CancellationToken cancellationToken = default);

        Task<CandleList> GetCandles(string figi, DateTime from, DateTime to, CandleInterval interval, CancellationToken cancellationToken = default);
    }
}
