using AutoMapper;
using MarketInstrumentDataService = DataService.Data.MarketInstrument;
using MarketInstrumentRepository = DataService.GetDataCacheHttpClient.Repository.Data.MarketInstrument;
using MarketInstrumentHttpClientTinkoff = DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data.MarketInstrument;
using CandleIntervalDataService = DataService.Data.CandleInterval;
using CandleIntervalRepository = DataService.GetDataCacheHttpClient.Repository.Data.CandleInterval;
using CandleIntervalHttpClientTinkof = DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data.CandleInterval;
using CandlePayloadDataService = DataService.Data.CandlePayload;
using CandlePayloadRepository = DataService.GetDataCacheHttpClient.Repository.Data.CandlePayload;
using CandlePayloadHttpClientTinkoff = DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data.CandlePayload;
using CurrencyDataService = DataService.Data.Currency;
using CurrencyRepository = DataService.GetDataCacheHttpClient.Repository.Data.Currency;
using CurrencyHttpClientTinkoff = DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data.Currency;
using InstrumentTypeDataService = DataService.Data.InstrumentType;
using InstrumentTypeRepository = DataService.GetDataCacheHttpClient.Repository.Data.InstrumentType;
using InstrumentTypeHttpClientTinkoff = DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data.InstrumentType;

namespace DataService.GetDataCacheHttpClient
{
    public class AutoMapperProfileGetDataCacheHttpClint : Profile
    {
        public AutoMapperProfileGetDataCacheHttpClint()
        {
            #region HttpDataToRepositoryData
            CreateMap<MarketInstrumentHttpClientTinkoff, MarketInstrumentRepository>();
            CreateMap<CandlePayloadHttpClientTinkoff, CandlePayloadRepository>();
            CreateMap<CandleIntervalHttpClientTinkof, CandleIntervalRepository>();
            CreateMap<CurrencyHttpClientTinkoff, CurrencyRepository>();
            CreateMap<InstrumentTypeHttpClientTinkoff, InstrumentTypeRepository>();
            #endregion

            #region RepositoryDataToDataServiceData
            CreateMap<MarketInstrumentRepository, MarketInstrumentDataService>();
            CreateMap<CandlePayloadRepository, CandlePayloadDataService>();
            CreateMap<CandleIntervalRepository, CandleIntervalDataService>();
            CreateMap<CurrencyRepository, CurrencyDataService>();
            CreateMap<InstrumentTypeRepository, InstrumentTypeDataService>();
            #endregion
        }
    }
}
