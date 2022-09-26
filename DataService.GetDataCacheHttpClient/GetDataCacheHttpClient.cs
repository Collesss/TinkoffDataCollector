using Common.Data;
using DataService.GetDataCacheHttpClient.HttpClient.Interfaces;
using DataService.GetDataCacheHttpClient.Repository.Interfaces;
using DataService.Interfaces;
using Microsoft.Extensions.Logging;

namespace TinkoffDataCollector.TinkoffDataService.Implementation.GetDataCacheHttpClient
{
    public class GetDataCacheHttpClient : ITinkoffDataService
    {
        private readonly ILogger<GetDataCacheHttpClient> _logger;
        private readonly IRepositoryCandlePayload _repositoryCandlePayload;
        private readonly IRepositoryMarketInstrument _repositoryMarketInstrument;
        private readonly IRepositoryDataAboutAlreadyLoaded _repositoryDataAboutAlreadyLoaded;
        private readonly IHttpClient _tinkoffHttpClient;

        public GetDataCacheHttpClient(ILogger<GetDataCacheHttpClient> logger, IRepositoryCandlePayload repositoryCandlePayload, 
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
