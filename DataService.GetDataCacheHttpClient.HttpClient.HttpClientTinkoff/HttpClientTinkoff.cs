using Common.Data;
using DataService.GetDataCacheHttpClient.HttpClient.Exceptions;
using DataService.GetDataCacheHttpClient.HttpClient.HttpClientTinkoff.Options;
using DataService.GetDataCacheHttpClient.HttpClient.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace DataService.GetDataCacheHttpClient.HttpClient.HttpClientTinkoff
{
    public class HttpClientTinkoff : IHttpClient
    {
        private readonly ILogger<HttpClientTinkoff> _logger;
        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly IOptions<HttpClientTinkoffOptions> _options;

        public HttpClientTinkoff(ILogger<HttpClientTinkoff> logger,
            System.Net.Http.HttpClient httpClient, IOptions<HttpClientTinkoffOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<IEnumerable<MarketInstrument>> GetStoks(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            try
            {
                response = await _httpClient.GetAsync(_options.Value.BaseMarketInstrumentsRoute, cancellationToken);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<MarketInstrument>>
                    (new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);
            }
            catch(Exception e)
            {
                throw new HttpClientException(e.Message, response.StatusCode.ToString(), "", response.StatusCode);
            }
        }

        public async Task<IEnumerable<CandlePayload>> GetCandles(string figi, DateTime from, DateTime to, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            try
            {
                //add full query parametrs
                response = await _httpClient.GetAsync(_options.Value.BaseCandlesRoute, cancellationToken);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<CandlePayload>>
                    (new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);
            }
            catch (Exception e)
            {
                throw new HttpClientException(e.Message, response.StatusCode.ToString(), "", response.StatusCode);
            }
        }
    }
}
