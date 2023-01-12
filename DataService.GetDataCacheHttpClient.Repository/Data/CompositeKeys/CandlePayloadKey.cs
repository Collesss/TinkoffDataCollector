namespace DataService.GetDataCacheHttpClient.Repository.Data.CompositeKeys
{
    public class CandlePayloadKey
    {
        public DateTime Time { get; }

        public CandleInterval Interval { get; }

        public string Figi { get; }
    }
}
