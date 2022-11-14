using AutoMapper;
using DataService.Exceptions;
using DataService.Interfaces;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SaveService.Exceptions;
using SaveService.Interfaces;
using TinkoffDataCollectorService.BaseSave.Options;
using TinkoffDataCollectorService.Exceptions;
using TinkoffDataCollectorService.Interfaces;
using CandlePayloadDataService = DataService.Data.CandlePayload;
using CandleIntervalDataService = DataService.Data.CandleInterval;
using CandlePayloadSaveService = SaveService.Data.CandlePayload;

namespace TinkoffDataCollectorService.BaseSave
{
    public class TinkoffDataCollectorService : ITinkoffDataCollectorService
    {
        private readonly ILogger<TinkoffDataCollectorService> _logger;
        private readonly ISaveService _saveService;
        private readonly ITinkoffDataService _tinkoffDataService;
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IOptions<TinkoffDataCollectorServiceOptions> _options;

        public TinkoffDataCollectorService(ILogger<TinkoffDataCollectorService> logger, ISaveService saveService,
            ITinkoffDataService tinkoffDataService, IStringLocalizer stringLocalizer,
            IMapper mapper, IOptions<TinkoffDataCollectorServiceOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _saveService = saveService ?? throw new ArgumentNullException(nameof(saveService));
            _tinkoffDataService = tinkoffDataService ?? throw new ArgumentNullException(nameof(tinkoffDataService));
            _stringLocalizer = stringLocalizer ?? throw new ArgumentNullException(nameof(stringLocalizer));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            try
            {
                foreach (var stock in await _tinkoffDataService.GetAllStoks(cancellationToken))
                {
                    IEnumerable<CandlePayloadSaveService> candles;

                    try
                    {
                        
                        candles = _mapper.Map<IEnumerable<CandlePayloadDataService>, IEnumerable<CandlePayloadSaveService>>
                            (await _tinkoffDataService.GetStockCandles(stock.Figi, _options.Value.From, _options.Value.To, 
                                CandleIntervalDataService.Hour, cancellationToken));
                    }
                    catch (TinkoffDataServiceException e)
                    {
                        _logger.LogError(e, _stringLocalizer["Programm.TinkoffDataServiceException:ErrorLoadCandles", e.Message]);
                        continue;
                    }

                    try
                    {
                        //await _saveService.Save(new SaveData(stock.Name, candles), cancellationToken);
                    }
                    catch (SaveServiceException e)
                    {
                        _logger.LogError(e, _stringLocalizer["Programm.SaveServiceException", e.Message]);
                        continue;
                    }
                }
            }
            catch (TinkoffDataServiceException e)
            {
                _logger.LogError(e, _stringLocalizer["Programm.TinkoffDataServiceException:ErrorLoadStoks", e.Message]);
                throw new TinkoffDataCollectorServiceException(e.Message, e);
            }
            catch (OperationCanceledException e)
            {
                _logger.LogError(e, _stringLocalizer["Programm.OperationCanceledException"]);
                throw new TinkoffDataCollectorServiceException(e.Message, e);
            }
        }
    }
}
