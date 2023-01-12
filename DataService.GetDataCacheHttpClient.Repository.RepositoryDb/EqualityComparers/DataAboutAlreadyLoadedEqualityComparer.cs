using DataService.GetDataCacheHttpClient.Repository.Data;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb.EqualityComparers
{
    public class DataAboutAlreadyLoadedEqualityComparer : IEqualityComparer<DataAboutAlreadyLoaded>
    {
        bool IEqualityComparer<DataAboutAlreadyLoaded>.Equals(DataAboutAlreadyLoaded x, DataAboutAlreadyLoaded y) =>
            x?.Equals(y) ?? false;


        int IEqualityComparer<DataAboutAlreadyLoaded>.GetHashCode(DataAboutAlreadyLoaded obj) =>
            obj?.GetHashCode() ?? 0;
    }
}
