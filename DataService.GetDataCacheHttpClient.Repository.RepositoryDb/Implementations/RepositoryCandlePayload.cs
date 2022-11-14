using DataService.GetDataCacheHttpClient.Repository.Data;
using DataService.GetDataCacheHttpClient.Repository.Data.CompositeKeys;
using DataService.GetDataCacheHttpClient.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb.Implementations
{
    public class RepositoryCandlePayload :
        Repository<CandlePayload, CandlePayloadKey, RepositoryDbContext>, IRepositoryCandlePayload
    {
        private readonly ILogger<RepositoryCandlePayload> _logger;

        public RepositoryCandlePayload(RepositoryDbContext dbConxtet,
            ILogger<RepositoryCandlePayload> logger,
            ILogger<Repository<CandlePayload, CandlePayloadKey, RepositoryDbContext>> baseRepositoryLogger) 
            : base(dbConxtet, baseRepositoryLogger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<CandlePayload>> GetCandlesStock(string figi, DateTime from, DateTime to, CancellationToken cancellationToken = default) =>
            await _dbConxtet.CandlePayloads
                .Where(candle => candle.Figi == figi && candle.Time >= from && candle.Time < to)
                .ToListAsync(cancellationToken);

        protected override async Task<CandlePayload> Find(CandlePayloadKey id, CancellationToken cancellationToken = default) =>
            await _dbConxtet.FindAsync<CandlePayload>(id.Figi, id.Interval, id.Time, cancellationToken);
    }
}
