using Common.Data;
using DataService.GetDataCacheHttpClient.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb.Implementations
{
    public class RepositoryMarketInstrument : 
        Repository<MarketInstrument, string, RepositoryDbContext>, IRepositoryMarketInstrument
    {
        private readonly ILogger<RepositoryMarketInstrument> _logger;

        public RepositoryMarketInstrument(RepositoryDbContext dbConxtet,
            ILogger<RepositoryMarketInstrument> logger,
            ILogger<Repository<MarketInstrument, string, RepositoryDbContext>> baseRepositoryLogger)
            : base(dbConxtet, baseRepositoryLogger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
