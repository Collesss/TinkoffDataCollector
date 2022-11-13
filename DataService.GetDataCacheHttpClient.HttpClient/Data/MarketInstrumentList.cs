using Common.Data;
using System.Text.Json.Serialization;

namespace DataService.GetDataCacheHttpClient.HttpClient.Data
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
