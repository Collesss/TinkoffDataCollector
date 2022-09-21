using TinkoffDataCollector.Common.Data;

namespace TinkoffDataCollector.SaveService.Interfaces
{
    public interface ISaveService
    {
        Task Save(MarketInstrument stock, IEnumerable<CandlePayload> candlePayloads, CancellationToken cancellationToken);
    }
}
