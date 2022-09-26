using Common.Data;

namespace SaveService.Interfaces
{
    public interface ISaveService
    {
        Task Save(MarketInstrument stock, IEnumerable<CandlePayload> candlePayloads, CancellationToken cancellationToken);
    }
}
