using Common.Data;

namespace DataService.GetDataCacheHttpClient.Repository.Interfaces
{
    public interface IRepositoryCandlePayload : IRepository<CandlePayload, string>
    {
        Task<IEnumerable<CandlePayload>> GetCandlesStock(string figi, DateTime from, DateTime to);
    }
}
