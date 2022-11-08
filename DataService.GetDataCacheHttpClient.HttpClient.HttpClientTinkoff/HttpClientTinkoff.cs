using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using DataService.GetDataCacheHttpClient.HttpClient.Interfaces;
using Microsoft.Extensions.Logging;

namespace DataService.GetDataCacheHttpClient.HttpClient.HttpClientTinkoff
{
    public class HttpClientTinkoff : IHttpClient
    {
        private readonly ILogger<HttpClientTinkoff> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientTinkoff(ILogger<HttpClientTinkoff> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public IEnumerable<CandlePayload> GetCandles(string figi, DateTime from, DateTime to, CancellationToken cancellationToken)
        {
            using var client = _httpClientFactory.CreateClient();

            

            throw new NotImplementedException();
        }

        public IEnumerable<MarketInstrument> GetStoks(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
