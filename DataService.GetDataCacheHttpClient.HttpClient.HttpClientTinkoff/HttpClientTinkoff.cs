using Common.Data;
using DataService.GetDataCacheHttpClient.HttpClient.HttpClientTinkoff.Options;
using DataService.GetDataCacheHttpClient.HttpClient.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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

        public IEnumerable<MarketInstrument> GetStoks(CancellationToken cancellationToken)
        {



            throw new NotImplementedException();
        }

        public IEnumerable<CandlePayload> GetCandles(string figi, DateTime from, DateTime to, CancellationToken cancellationToken)
        {



            throw new NotImplementedException();
        }
    }
}
