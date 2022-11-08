﻿using Common.Data;
using DataService.GetDataCacheHttpClient.Repository.Data.CompositeKeys;

namespace DataService.GetDataCacheHttpClient.Repository.Interfaces
{
    public interface IRepositoryCandlePayload : IRepository<CandlePayload, CandlePayloadKey>
    {
        Task<IEnumerable<CandlePayload>> GetCandlesStock(string figi, DateTime from, DateTime to, CancellationToken cancellationToken = default);
    }
}