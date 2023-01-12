using DataService.GetDataCacheHttpClient.Repository.Data;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb.EqualityComparers
{
    public class CandlePayloadKeyEqualityComparer : IEqualityComparer<CandlePayload>
    {
        bool IEqualityComparer<CandlePayload>.Equals(CandlePayload x, CandlePayload y) =>
            x != null &&
            y != null &&
            x.Figi == y.Figi &&
            x.Time == y.Time &&
            x.Interval == y.Interval;

        int IEqualityComparer<CandlePayload>.GetHashCode(CandlePayload obj) =>
            obj.Figi.GetHashCode() ^ (~obj.Interval.GetHashCode() * (int)obj.Time.Ticks);
    }
}
