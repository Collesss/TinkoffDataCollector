using AutoMapper;
using DataService.Exceptions;
using DataService.GetDataCacheHttpClient.HttpClientTinkoff.Interfaces;
using DataService.GetDataCacheHttpClient.Repository.Interfaces;
using DataService.Interfaces;
using Microsoft.Extensions.Logging;
using MarketInstrumentDataService = DataService.Data.MarketInstrument;
using MarketInstrumentRepositoryService = DataService.GetDataCacheHttpClient.Repository.Data.MarketInstrument;
using MarketInstrumentHttpClientTinkoff = DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data.MarketInstrument;
using CandleIntervalHttpClientTinkof = DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data.CandleInterval;
using CandleIntervalRepository = DataService.GetDataCacheHttpClient.Repository.Data.CandleInterval;
using CandleIntervalDataService = DataService.Data.CandleInterval;
using CandlePayloadDataService = DataService.Data.CandlePayload;
using CandlePayloadHttpClientTinkoff = DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data.CandlePayload;
using CandlePayloadRepository = DataService.GetDataCacheHttpClient.Repository.Data.CandlePayload;
using DataAboutAlreadyLoadedRepository = DataService.GetDataCacheHttpClient.Repository.Data.DataAboutAlreadyLoaded;
using DataService.GetDataCacheHttpClient;

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
            IRepositoryMarketInstrument repositoryMarketInstrument, IRepositoryDataAboutAlreadyLoaded repositoryDataAboutAlreadyLoaded,
            IHttpClientTinkoff tinkoffHttpClient, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repositoryCandlePayload = repositoryCandlePayload ?? throw new ArgumentNullException(nameof(repositoryCandlePayload));
            _repositoryMarketInstrument = repositoryMarketInstrument ?? throw new ArgumentNullException(nameof(repositoryMarketInstrument));
            _repositoryDataAboutAlreadyLoaded = repositoryDataAboutAlreadyLoaded ?? throw new ArgumentNullException(nameof(repositoryDataAboutAlreadyLoaded));
            _tinkoffHttpClient = tinkoffHttpClient ?? throw new ArgumentNullException(nameof(tinkoffHttpClient));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<MarketInstrumentDataService>> GetAllStoks(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<MarketInstrumentHttpClientTinkoff> marketInstruments = (await _tinkoffHttpClient.GetStoks(cancellationToken)).Instruments;

                await _repositoryMarketInstrument.Merge(_mapper.Map<IEnumerable<MarketInstrumentHttpClientTinkoff>, 
                    IEnumerable<MarketInstrumentRepositoryService>>(marketInstruments).ToList(), cancellationToken);
            }
            catch(OperationCanceledException)
            {
                throw;
            }
            catch(Exception e)
            {
                _logger.LogError($"Error get info about stocks: {e.Message}");

                throw new TinkoffDataServiceException(e.Message, e);
            }

            return _mapper.Map<IEnumerable<MarketInstrumentRepositoryService>, IEnumerable<MarketInstrumentDataService>>
                (await _repositoryMarketInstrument.GetAll(cancellationToken));
        }


        private async Task<IEnumerable<(DateTime from, DateTime to)>> GetUnloadIntervals(string figi, DateTime from, DateTime to, CandleIntervalRepository interval, CancellationToken cancellationToken)
        {
            if (to > DateTime.UtcNow.Date.AddDays(1))
                to = DateTime.UtcNow.Date.AddDays(1);

            if (from >= to)
                return new List<(DateTime from, DateTime to)>();

            from = from.AddDays(-1);

            DateTime last = from;

            int group = 0;

            return (await _repositoryDataAboutAlreadyLoaded.GetDataAboutAlreadyLoadedStock(figi, from, to, interval, cancellationToken))
                .Select(dAL => dAL.Time)
                .OrderBy(time => time)
                .Prepend(from)
                .Append(to)
                .GroupBy(time =>
                {
                    bool incGroup = time - last < TimeSpan.FromDays(1);
                    last = time;
                    return incGroup ? ++group : group;
                })
                .Where(gr => gr.Count() > 1)
                .Select(gr => (from: gr.Min().AddDays(1), to: gr.Max()));
        }

        public async Task<IEnumerable<CandlePayloadDataService>> GetStockCandles(string figi, DateTime from, DateTime to, CandleIntervalDataService interval, CancellationToken cancellationToken)
        {
            //IEnumerable<CandlePayloadDataService> candlePayloads = null;

            try
            {
                IEnumerable<(DateTime from, DateTime to)> unloadIntervals = await GetUnloadIntervals(figi, from, to,
                    _mapper.Map<CandleIntervalDataService, CandleIntervalRepository>(interval), cancellationToken);

                IEnumerable<CandlePayloadRepository> candlePayloads = _mapper.Map<IEnumerable<CandlePayloadHttpClientTinkoff>, IEnumerable<CandlePayloadRepository>>
                    (unloadIntervals.SelectMany(ui => new TinkoffCuterRequest(interval, ui.from, ui.to))
                    .SelectMany(ui => _tinkoffHttpClient.GetCandles(figi, ui.from, ui.to,
                    _mapper.Map<CandleIntervalDataService, CandleIntervalHttpClientTinkof>(interval), cancellationToken).Result.Candles));

                await _repositoryCandlePayload.Merge(candlePayloads.ToList(), cancellationToken);

                await SetLoadInterval(figi, from, to, _mapper.Map<CandleIntervalDataService, CandleIntervalRepository>(interval), cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error get info about candles: {e.Message}");

                throw new TinkoffDataServiceException(e.Message, e);
            }

            return _mapper.Map<IEnumerable<CandlePayloadRepository>, IEnumerable<CandlePayloadDataService>>
                (await _repositoryCandlePayload.GetCandlesStock(figi, from, to, _mapper.Map<CandleIntervalDataService, CandleIntervalRepository>(interval), cancellationToken));
        }

        
        async Task SetLoadInterval(string figi, DateTime from, DateTime to, CandleIntervalRepository interval, CancellationToken cancellationToken = default)
        {
            if (to > DateTime.UtcNow.Date.AddDays(-1))
                to = DateTime.UtcNow.Date.AddDays(-1);

            if (from >= to)
                return;

            await _repositoryDataAboutAlreadyLoaded.Merge(Enumerable.Range(0, (to - from).Days)
                .Select(i => new DataAboutAlreadyLoadedRepository(figi, from.AddDays(i), interval)).ToList(),
               cancellationToken);
        }
    }
}
