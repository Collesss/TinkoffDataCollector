using DataService.GetDataCacheHttpClient.Repository.Data;
using DataService.GetDataCacheHttpClient.Repository.Interfaces;
using DataService.GetDataCacheHttpClient.Repository.RepositoryDb.EqualityComparers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb.Implementations
{
    public class RepositoryDataAboutAlreadyLoaded : 
        Repository<DataAboutAlreadyLoaded, DataAboutAlreadyLoaded, RepositoryDbContext, DataAboutAlreadyLoadedEqualityComparer, DataAboutAlreadyLoadedKeyEqualityComparer>, 
        IRepositoryDataAboutAlreadyLoaded
    {
        private readonly ILogger<RepositoryDataAboutAlreadyLoaded> _logger;

        public RepositoryDataAboutAlreadyLoaded(RepositoryDbContext dbConxtet,
            ILogger<RepositoryDataAboutAlreadyLoaded> logger,
            ILogger<Repository<DataAboutAlreadyLoaded, DataAboutAlreadyLoaded, RepositoryDbContext, DataAboutAlreadyLoadedEqualityComparer, DataAboutAlreadyLoadedKeyEqualityComparer>> baseRepositoryLogger)
            : base(dbConxtet, baseRepositoryLogger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task<DataAboutAlreadyLoaded> Find(DataAboutAlreadyLoaded id, CancellationToken cancellationToken = default) =>
            await _dbConxtet.FindAsync<DataAboutAlreadyLoaded>(id.Figi, id.Interval, id.Time, cancellationToken);


        public async Task<IEnumerable<DataAboutAlreadyLoaded>> GetDataAboutAlreadyLoadedStock(string figi, DateTime from, DateTime to, CandleInterval interval, CancellationToken cancellationToken = default) =>
            await _dbConxtet.DataAboutAlreadyLoadeds
                .Where(daal => daal.Figi == figi && daal.Interval == interval && daal.Time >= from && daal.Time < to)
                .ToListAsync(cancellationToken);
    }
}
