using Microsoft.Extensions.Logging;
using TinkoffDataCollector.Common.Data;
using TinkoffDataCollector.TinkoffDataService.Implementation.GetDataCacheHttpClient.Repository.Interfaces;
using TinkoffDataCollector.TinkoffDataService.Implementation.GetDataCacheHttpClient.TinkoffHttpClient.Interfaces;
using TinkoffDataCollector.TinkoffDataService.Interfaces;

namespace TinkoffDataCollector.TinkoffDataService.Implementation.GetDataCacheHttpClient
{
    public class TinkoffDataService : ITinkoffDataService
    {
        private readonly ILogger<TinkoffDataService> _logger;
        private readonly IRepositoryCandlePayload _repositoryCandlePayload;
        private readonly IRepositoryMarketInstrument _repositoryMarketInstrument;
        private readonly IRepositoryDataAboutAlreadyLoaded _repositoryDataAboutAlreadyLoaded;
        private readonly ITinkoffHttpClient _tinkoffHttpClient;
        public TinkoffDataService(ILogger<TinkoffDataService> logger, IRepositoryCandlePayload repositoryCandlePayload, 
            IRepositoryMarketInstrument repositoryMarketInstrument, IRepositoryDataAboutAlreadyLoaded repositoryDataAboutAlreadyLoaded)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repositoryCandlePayload = repositoryCandlePayload ?? throw new ArgumentNullException(nameof(repositoryCandlePayload));
            _repositoryMarketInstrument = repositoryMarketInstrument ?? throw new ArgumentNullException(nameof(repositoryMarketInstrument));
            _repositoryDataAboutAlreadyLoaded = repositoryDataAboutAlreadyLoaded ?? throw new ArgumentNullException(nameof(repositoryDataAboutAlreadyLoaded));
        }

        public Task<IEnumerable<MarketInstrument>> GetAllStoks(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CandlePayload>> GetStockCandles(string figi, DateTime from, DateTime to, CandleInterval interval, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
