using System.Text.Json.Serialization;

namespace DataService.GetDataCacheHttpClient.HttpClient.Data
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
