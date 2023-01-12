using DataService.GetDataCacheHttpClient.Repository.Data;

namespace DataService.GetDataCacheHttpClient.Repository.Interfaces
{
    public interface IRepositoryDataAboutAlreadyLoaded : IRepository<DataAboutAlreadyLoaded, DataAboutAlreadyLoaded>
    {
        Task<IEnumerable<DataAboutAlreadyLoaded>> GetDataAboutAlreadyLoadedStock(string figi, DateTime from, DateTime to, CandleInterval interval, CancellationToken cancellationToken = default);
    }
}
