using System.Text.Json.Serialization;

namespace DataService.GetDataCacheHttpClient.HttpClient.Data
{
    public class CandleList
    {
        public string Figi { get; }

        public CandleInterval Interval { get; }

        public List<CandlePayload> Candles { get; }

        [JsonConstructor]
        public CandleList(string figi, CandleInterval interval, List<CandlePayload> candles)
        {
            Figi = figi;
            Interval = interval;
            Candles = candles;
        }
    }
}
