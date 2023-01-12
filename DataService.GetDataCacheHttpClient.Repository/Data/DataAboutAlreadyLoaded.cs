namespace DataService.GetDataCacheHttpClient.Repository.Data
{
    public class DataAboutAlreadyLoaded : IEquatable<DataAboutAlreadyLoaded>
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

        bool IEquatable<DataAboutAlreadyLoaded>.Equals(DataAboutAlreadyLoaded other) =>
            other != null &&
            this.Figi == other.Figi &&
            this.Time == other.Time &&
            this.Interval == other.Interval;

        public override bool Equals(object obj) =>
            obj is DataAboutAlreadyLoaded dataLoaded &&
            ((IEquatable<DataAboutAlreadyLoaded>)this).Equals(dataLoaded);

        public override int GetHashCode() =>
            ((int)(Time.Ticks * 2) + (~(int)Interval)) * Figi.GetHashCode();
    }
}
