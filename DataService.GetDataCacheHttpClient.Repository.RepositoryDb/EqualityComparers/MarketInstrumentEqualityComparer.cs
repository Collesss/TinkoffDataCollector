using DataService.GetDataCacheHttpClient.Repository.Data;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb.EqualityComparers
{
    public class MarketInstrumentEqualityComparer : IEqualityComparer<MarketInstrument>
    {
        bool IEqualityComparer<MarketInstrument>.Equals(MarketInstrument x, MarketInstrument y) =>
            x?.Equals(y) ?? false;


        int IEqualityComparer<MarketInstrument>.GetHashCode(MarketInstrument obj) =>
            obj?.GetHashCode() ?? 0;
    }
}
