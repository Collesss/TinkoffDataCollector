namespace SaveService.Data
{
    public class SaveData
    {
        public string StockName { get; }

        public IEnumerable<CandlePayload> CandlePayloads { get; }

        public SaveData(string stockName, IEnumerable<CandlePayload> candlePayloads)
        {
            StockName = stockName;
            CandlePayloads = candlePayloads;
        }
    }
}
