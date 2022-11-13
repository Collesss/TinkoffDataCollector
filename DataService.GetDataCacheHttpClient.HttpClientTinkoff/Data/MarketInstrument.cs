using System.Text.Json.Serialization;

namespace DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data
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

        [JsonConstructor]
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

        public override string ToString()
        {
            return string.Format("{0}: {1}, {2}: {3}, {4}: {5}, {6}: {7}, {8}: {9}, {10}: {11}, {12}: {13}, {14}: {15}", "Figi", Figi, "Ticker", Ticker, "Isin", Isin, "MinPriceIncrement", MinPriceIncrement, "Lot", Lot, "Currency", Currency, "Name", Name, "Type", Type);
        }
    }
}
