using DataService.GetDataCacheHttpClient.Repository.Data;
using DataService.GetDataCacheHttpClient.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb.Implementations
{
    public class RepositoryDataAboutAlreadyLoaded : 
        Repository<DataAboutAlreadyLoaded, DataAboutAlreadyLoaded, RepositoryDbContext>, 
        IRepositoryDataAboutAlreadyLoaded
    {
        private readonly ILogger<RepositoryDataAboutAlreadyLoaded> _logger;

        public RepositoryDataAboutAlreadyLoaded(RepositoryDbContext dbConxtet,
            ILogger<RepositoryDataAboutAlreadyLoaded> logger,
            ILogger<Repository<DataAboutAlreadyLoaded, DataAboutAlreadyLoaded, RepositoryDbContext>> baseRepositoryLogger)
            : base(dbConxtet, baseRepositoryLogger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task<DataAboutAlreadyLoaded> Find(DataAboutAlreadyLoaded id, CancellationToken cancellationToken = default) =>
            await _dbConxtet.FindAsync<DataAboutAlreadyLoaded>(id.Figi, id.Interval, id.Time, cancellationToken);
        
    }
}
