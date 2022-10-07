namespace SaveService.Data
{
    public class CandlePayload
    {
        public decimal Open { get; }

        public decimal Close { get; }

        public decimal High { get; }

        public decimal Low { get; }

        public DateTime CloseTime { get; }

        public CandlePayload(decimal open, decimal close, decimal high, decimal low, DateTime closeTime)
        {
            Open = open;
            Close = close;
            High = high;
            Low = low;
            CloseTime = closeTime;
        }
    }
}
