using DataService.GetDataCacheHttpClient.Repository.Data;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb.EqualityComparers
{
    public class CandlePayloadEqualityComparer : IEqualityComparer<CandlePayload>
    {
        bool IEqualityComparer<CandlePayload>.Equals(CandlePayload x, CandlePayload y) =>
            x?.Equals(y) ?? false;


        int IEqualityComparer<CandlePayload>.GetHashCode(CandlePayload obj) =>
            obj?.GetHashCode() ?? 0;
    }
}
