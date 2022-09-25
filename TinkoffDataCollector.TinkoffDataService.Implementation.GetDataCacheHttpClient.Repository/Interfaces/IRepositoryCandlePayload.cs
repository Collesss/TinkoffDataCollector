using TinkoffDataCollector.Common.Data;

namespace TinkoffDataCollector.TinkoffDataService.Implementation.GetDataCacheHttpClient.Repository.Interfaces
{
    public interface IRepositoryCandlePayload : IRepository<CandlePayload, string>
    {
        Task<IEnumerable<CandlePayload>> GetCandlesStock(string figi, DateTime from, DateTime to);
    }
}
