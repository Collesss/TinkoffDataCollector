using DataService.GetDataCacheHttpClient.Repository.Data;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb.EqualityComparers
{
    public class MarketInstrumentKeyEqualityComparer : IEqualityComparer<MarketInstrument>
    {
        bool IEqualityComparer<MarketInstrument>.Equals(MarketInstrument x, MarketInstrument y) =>
            x != null &&
            y != null &&
            x.Figi == y.Figi &&
            //x.Ticker == y.Ticker &&
            x.Isin == y.Isin;

        int IEqualityComparer<MarketInstrument>.GetHashCode(MarketInstrument obj) =>
            obj.Figi.GetHashCode() ^ obj.Isin.GetHashCode();
    }
}
