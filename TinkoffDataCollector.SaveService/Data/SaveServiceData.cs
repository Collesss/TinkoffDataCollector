using TinkoffDataCollector.TinkoffDataService.Data;

namespace TinkoffDataCollector.SaveService.Data
{
    public class SaveServiceData
    {
        public MarketInstrument Stock { get; }
        public IEnumerable<CandlePayload> CandlePayloads { get; }

        public SaveServiceData(MarketInstrument stock, IEnumerable<CandlePayload> candlePayloads)
        {
            Stock = stock;
            CandlePayloads = candlePayloads;
        }
    }
}
