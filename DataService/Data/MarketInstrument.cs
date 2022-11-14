namespace DataService.Data
{
    public class MarketInstrument
    {
        public string Figi { get; }

        public string Ticker { get; }

        public string Isin { get; }

        public decimal MinPriceIncrement { get; }

        public int Lot { get; }

        public Currency Currency { get; }

        public string Name { get; }

        public InstrumentType Type { get; }

        public MarketInstrument(string figi, string ticker, string isin, decimal minPriceIncrement, int lot, Currency currency, string name, InstrumentType type)
        {
            Figi = figi;
            Ticker = ticker;
            Isin = isin;
            MinPriceIncrement = minPriceIncrement;
            Lot = lot;
            Currency = currency;
            Name = name;
            Type = type;
        }
    }
}
