using DataService.GetDataCacheHttpClient.HttpClientTinkoff.Extensions;
using DataService.GetDataCacheHttpClient.HttpClientTinkoff.Data;
using DataService.GetDataCacheHttpClient.HttpClientTinkoff.Exceptions;
using DataService.GetDataCacheHttpClient.HttpClientTinkoff.HttpClientTinkoffBase.Options;
using DataService.GetDataCacheHttpClient.HttpClientTinkoff.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;

namespace DataService.GetDataCacheHttpClient.HttpClientTinkoff.HttpClientTinkoffBase
{
    public class HttpClientTinkoff : IHttpClientTinkoff
    {
        private readonly ILogger<HttpClientTinkoff> _logger;
        private readonly HttpClient _httpClient;
        private readonly IOptions<HttpClientTinkoffOptions> _options;

        public HttpClientTinkoff(ILogger<HttpClientTinkoff> logger,
            HttpClient httpClient, IOptions<HttpClientTinkoffOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<MarketInstrumentList> GetStoks(CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response = null;

            try
            {
                response = await _httpClient.GetAsync(_options.Value.BaseMarketInstrumentsRoute, cancellationToken);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<MarketInstrumentList>
                    (new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);
            }
            catch(OperationCanceledException)
            {
                throw;
            }
            catch(Exception e)
            {
                throw new HttpClientException(e.Message, response.StatusCode.ToString(), "", response.StatusCode);
            }
        }

        public async Task<CandleList> GetCandles(string figi, DateTime from, DateTime to, CandleInterval interval, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response = null;

            static string TimeFormatUrl(DateTime dateTime) =>
                HttpUtility.UrlEncode(dateTime.ToString("O"));

            try
            {
                string url = $@"{_options.Value.BaseCandlesRoute}?figi={figi}&from={TimeFormatUrl(from)}&to={TimeFormatUrl(to)}&interval={interval.GetEnumMemberValue()}";
                
                response = await _httpClient.GetAsync(url, cancellationToken);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<CandleList>
                    (new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new HttpClientException(e.Message, response.StatusCode.ToString(), "", response.StatusCode);
            }
        }
    }
}
