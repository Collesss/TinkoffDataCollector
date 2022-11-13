using System.Text.Json.Serialization;

namespace DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data
{
    public class MarketInstrumentList
    {
        public int Total { get; }

        public List<MarketInstrument> Instruments { get; }

        [JsonConstructor]
        public MarketInstrumentList(int total, List<MarketInstrument> instruments)
        {
            Total = total;
            Instruments = instruments;
        }
    }
}
