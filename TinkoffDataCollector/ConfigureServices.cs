using DataService.GetDataCacheHttpClient.HttpClientTinkoff.HttpClientTinkoffBase;
using DataService.GetDataCacheHttpClient.HttpClientTinkoff.HttpClientTinkoffBase.Options;
using DataService.GetDataCacheHttpClient.HttpClientTinkoff.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using System.Net;
using System.Net.Http.Headers;

namespace TinkoffDataCollector
{
    internal partial class Program
    {
        static void ConfigureServices(IServiceCollection serviceProvider)
        {
            serviceProvider
                .AddSingleton(Configuration)
                .AddHttpClient<IHttpClientTinkoff, HttpClientTinkoff>((sp, client) =>
                {
                    IOptions<HttpClientTinkoffOptions> options = sp.GetRequiredService<IOptions<HttpClientTinkoffOptions>>();

                    client.BaseAddress = new Uri(options.Value.BaseAddress);
                    client.DefaultRequestVersion = new Version(1, 1);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.Value.Token);
                })
                .AddPolicyHandler(GetPolicy());

            static AsyncRetryPolicy<HttpResponseMessage> GetPolicy() =>
                Policy
                    .HandleResult<HttpResponseMessage>(response => response.StatusCode == HttpStatusCode.TooManyRequests)
                    .WaitAndRetryForeverAsync(retryAttempt => TimeSpan.FromSeconds(20 * retryAttempt));
        }
    }
}
