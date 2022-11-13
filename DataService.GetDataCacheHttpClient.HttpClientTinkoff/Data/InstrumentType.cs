using System.Text.Json.Serialization;

namespace DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum InstrumentType
    {
        Stock,
        Currency,
        Bond,
        Etf
    }
}
