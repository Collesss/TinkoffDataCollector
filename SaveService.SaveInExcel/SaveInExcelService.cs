using Common.Data;
using SaveService.Interfaces;

namespace SaveService.SaveInExcel
{
    public class SaveInExcelService : ISaveService
    {
        public Task Save(MarketInstrument stock, IEnumerable<CandlePayload> candlePayloads, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
