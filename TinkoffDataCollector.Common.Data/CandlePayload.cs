namespace TinkoffDataCollector.Common.Data
{
    public class CandlePayload
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
    }
}
