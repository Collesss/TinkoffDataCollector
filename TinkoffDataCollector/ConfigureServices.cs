using DataService.GetDataCacheHttpClient.HttpClientTinkoff.HttpClientTinkoffBase;
using DataService.GetDataCacheHttpClient.HttpClientTinkoff.HttpClientTinkoffBase.Options;
using DataService.GetDataCacheHttpClient.HttpClientTinkoff.Interfaces;
using DataService.GetDataCacheHttpClient.Repository.Interfaces;
using DataService.GetDataCacheHttpClient.Repository.RepositoryDb;
using DataService.GetDataCacheHttpClient.Repository.RepositoryDb.Implementations;
using DataService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using System.Net;
using System.Net.Http.Headers;
using TinkoffDataCollector.TinkoffDataService.Implementation.GetDataCacheHttpClient;
using DataService.GetDataCacheHttpClient;
using Microsoft.Extensions.Logging;
using SaveService.Interfaces;
using SaveService.SaveInExcel;
using SaveService.SaveInExcel.Options;
using TinkoffDataCollectorService.BaseSave.Options;
using TinkoffDataCollectorService.Interfaces;

namespace TinkoffDataCollector
{
    internal partial class Program
    {
        static void ConfigureServices(IServiceCollection serviceProvider)
        {
            serviceProvider
                .AddSingleton(Configuration)
                .Configure<SaveInExelOptions>(Configuration.GetSection("SaveInExcelOptions"))
                .Configure<TinkoffDataCollectorServiceOptions>(Configuration.GetSection("TinkoffDataCollectorServiceOptions"))
                .Configure<HttpClientTinkoffOptions>(Configuration.GetSection("HttpClientTinkoffOptions"))
                .AddLogging(cfg =>
                {
                    cfg.SetMinimumLevel(LogLevel.Information); 
                    cfg.AddConsole();
                })
                .AddScoped<ITinkoffDataCollectorService, TinkoffDataCollectorService.BaseSave.TinkoffDataCollectorService>()
                .AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfileGetDataCacheHttpClint>())
                .AddScoped<ITinkoffDataService, GetDataCacheHttpClient>()
                .AddScoped<IRepositoryCandlePayload, RepositoryCandlePayload>()
                .AddScoped<IRepositoryMarketInstrument, RepositoryMarketInstrument>()
                .AddScoped<IRepositoryDataAboutAlreadyLoaded, RepositoryDataAboutAlreadyLoaded>()
                .AddDbContext<RepositoryDbContext>((services, opts) => 
                {
                    opts.UseSqlite(services.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection"));
                    opts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);    
                })
                .AddScoped<ISaveService, SaveInExcelService>()
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
