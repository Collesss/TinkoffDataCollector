namespace DataService.GetDataCacheHttpClient.Repository.Data
{
    public class CandlePayload : IEquatable<CandlePayload>
    {
        public decimal Open { get; }

        public decimal Close { get; }

        public decimal High { get; }

        public decimal Low { get; }

        public decimal Volume { get; }

        public DateTime Time { get; }

        public CandleInterval Interval { get; }

        public string Figi { get; }

        public CandlePayload(decimal open, decimal close, decimal high, decimal low, decimal volume, DateTime time, CandleInterval interval, string figi)
        {
            Open = open;
            Close = close;
            High = high;
            Low = low;
            Volume = volume;
            Time = time;
            Interval = interval;
            Figi = figi;
        }

        bool IEquatable<CandlePayload>.Equals(CandlePayload other) =>
            other != null &&
            this.Close == other.Close &&
            this.Open == other.Open &&
            this.Low == other.Low &&
            this.High == other.High &&
            this.Volume == other.Volume &&
            this.Time == other.Time &&
            this.Figi == other.Figi &&
            this.Interval == other.Interval;

        public override bool Equals(object obj) =>
            obj is CandlePayload candle &&
            ((IEquatable<CandlePayload>)this).Equals(candle);

        public override int GetHashCode() =>
            ((int)(Close * 2 + Open * 3 + Low * 4 + High * 5 + Volume * 6) + (int)(Time.Ticks * 7) + (~(int)Interval)) * Figi.GetHashCode();
    }
}
