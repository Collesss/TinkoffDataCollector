using DataService.GetDataCacheHttpClient.HttpClient.HttpClientTinkoff;
using DataService.GetDataCacheHttpClient.HttpClient.HttpClientTinkoff.Options;
using DataService.GetDataCacheHttpClient.HttpClient.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace TinkoffDataCollector
{
    internal partial class Program
    {
        static void ConfigureServices(IServiceCollection serviceProvider)
        {
            serviceProvider
                .AddSingleton(Configuration)
                .AddHttpClient<IHttpClient, HttpClientTinkoff>((sp, client) =>
                {
                    IOptions<HttpClientTinkoffOptions> options = sp.GetRequiredService<IOptions<HttpClientTinkoffOptions>>();

                    client.BaseAddress = new Uri(options.Value.BaseAddress);
                    client.DefaultRequestVersion = new Version(1, 1);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.Value.Token);
                });
        }
    }
}
