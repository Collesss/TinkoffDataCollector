namespace DataService.GetDataCacheHttpClient.Repository.Data
{
    public class MarketInstrument : IEquatable<MarketInstrument>
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

        bool IEquatable<MarketInstrument>.Equals(MarketInstrument other) =>
            other != null &&
            this.Figi == other.Figi &&
            this.Ticker == other.Ticker &&
            this.Isin == other.Isin &&
            this.MinPriceIncrement == other.MinPriceIncrement &&
            this.Lot == other.Lot &&
            this.Currency == other.Currency &&
            this.Name == other.Name &&
            this.Type == other.Type;

        public override bool Equals(object obj) =>
            obj is MarketInstrument stock &&
            ((IEquatable<MarketInstrument>)this).Equals(stock);

        public override int GetHashCode() =>
            (int)(MinPriceIncrement * 2) + Lot * 3 + (~(int)Currency * ~(int)Type) * Name.GetHashCode() * Figi.GetHashCode() * Ticker.GetHashCode() * Isin.GetHashCode();
    }
}
