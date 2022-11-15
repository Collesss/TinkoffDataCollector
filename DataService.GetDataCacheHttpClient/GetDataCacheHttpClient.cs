using AutoMapper;
using DataService.Exceptions;
using DataService.GetDataCacheHttpClient.HttpClientTinkoff.Interfaces;
using DataService.GetDataCacheHttpClient.Repository.Interfaces;
using DataService.Interfaces;
using Microsoft.Extensions.Logging;
using MarketInstrumentDataService = DataService.Data.MarketInstrument;
using CandlePayloadDataService = DataService.Data.CandlePayload;
using CandleIntervalDataService = DataService.Data.CandleInterval;
using MarketInstrumentHttpClientTinkoff = DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data.MarketInstrument;
using CandlePayloadHttpClientTinkoff = DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data.CandlePayload;
using CandleIntervalHttpClientTinkof = DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data.CandleInterval;
using DataService.GetDataCacheHttpClient;
using System.Linq;
using System;

namespace TinkoffDataCollector.TinkoffDataService.Implementation.GetDataCacheHttpClient
{
    public class GetDataCacheHttpClient : ITinkoffDataService
    {
        private readonly ILogger<GetDataCacheHttpClient> _logger;
        private readonly IRepositoryCandlePayload _repositoryCandlePayload;
        private readonly IRepositoryMarketInstrument _repositoryMarketInstrument;
        private readonly IRepositoryDataAboutAlreadyLoaded _repositoryDataAboutAlreadyLoaded;
        private readonly IHttpClientTinkoff _tinkoffHttpClient;
        private readonly IMapper _mapper;

        public GetDataCacheHttpClient(ILogger<GetDataCacheHttpClient> logger, IRepositoryCandlePayload repositoryCandlePayload, 
            IRepositoryMarketInstrument repositoryMarketInstrument, IRepositoryDataAboutAlreadyLoaded repositoryDataAboutAlreadyLoaded, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repositoryCandlePayload = repositoryCandlePayload ?? throw new ArgumentNullException(nameof(repositoryCandlePayload));
            _repositoryMarketInstrument = repositoryMarketInstrument ?? throw new ArgumentNullException(nameof(repositoryMarketInstrument));
            _repositoryDataAboutAlreadyLoaded = repositoryDataAboutAlreadyLoaded ?? throw new ArgumentNullException(nameof(repositoryDataAboutAlreadyLoaded));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<MarketInstrumentDataService>> GetAllStoks(CancellationToken cancellationToken)
        {
            IEnumerable<MarketInstrumentDataService> marketInstruments = null;

            try
            {
                marketInstruments = _mapper.Map<IEnumerable<MarketInstrumentHttpClientTinkoff>, IEnumerable<MarketInstrumentDataService>>
                    ((await _tinkoffHttpClient.GetStoks(cancellationToken)).Instruments);


                throw new Exception("Need Merge In DB.");

            }
            catch(Exception e)
            {
                _logger.LogError($"Error get info about stocks: {e.Message}");

                throw new TinkoffDataServiceException(e.Message, e);
            }


            return marketInstruments;
        }

        public async Task<IEnumerable<CandlePayloadDataService>> GetStockCandles(string figi, DateTime from, DateTime to, CandleIntervalDataService interval, CancellationToken cancellationToken)
        {
            IEnumerable<CandlePayloadDataService> candlePayloads = null;

            try
            {
                /*
                TinkoffCuterRequest requests = new TinkoffCuterRequest(interval, from, to);

                candlePayloads = _mapper.Map<IEnumerable<CandlePayloadHttpClientTinkoff>, IEnumerable<CandlePayloadDataService>>
                    (requests.SelectMany(request => _tinkoffHttpClient.GetCandles(figi, request.from, request.to, 
                    _mapper.Map<CandleIntervalDataService, CandleIntervalHttpClientTinkof>(interval)).Result.Candles));
                */
            }
            catch (Exception e)
            {
                _logger.LogError($"Error get info about candles: {e.Message}");

                throw new TinkoffDataServiceException(e.Message, e);
            }

            return candlePayloads;
        }
    }
}
