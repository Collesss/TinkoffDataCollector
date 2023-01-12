using DataService.GetDataCacheHttpClient.Repository.Data;
using DataService.GetDataCacheHttpClient.Repository.Interfaces;
using DataService.GetDataCacheHttpClient.Repository.RepositoryDb.EqualityComparers;
using Microsoft.Extensions.Logging;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb.Implementations
{
    public class RepositoryMarketInstrument : 
        Repository<MarketInstrument, string, RepositoryDbContext, MarketInstrumentEqualityComparer, MarketInstrumentKeyEqualityComparer>, IRepositoryMarketInstrument
    {
        private readonly ILogger<RepositoryMarketInstrument> _logger;

        public RepositoryMarketInstrument(RepositoryDbContext dbConxtet,
            ILogger<RepositoryMarketInstrument> logger,
            ILogger<Repository<MarketInstrument, string, RepositoryDbContext, MarketInstrumentEqualityComparer, MarketInstrumentKeyEqualityComparer>> baseRepositoryLogger)
            : base(dbConxtet, baseRepositoryLogger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
