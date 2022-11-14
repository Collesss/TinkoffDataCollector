namespace DataService.GetDataCacheHttpClient.Repository.Data
{
    public class DataAboutAlreadyLoaded
    {
        public string Figi { get; }
        public DateTime Time { get; }
        public CandleInterval Interval { get; }

        public DataAboutAlreadyLoaded(string figi, DateTime time, CandleInterval interval)
        {
            Figi = figi;
            Time = time;
            Interval = interval;
        }
    }
}
