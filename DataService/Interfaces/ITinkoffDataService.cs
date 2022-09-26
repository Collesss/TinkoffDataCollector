using Common.Data;

namespace DataService.Interfaces
{
    public interface ITinkoffDataService
    {
        /// <summary>
        /// return info about all stock
        /// </summary>
        /// <exception cref="Exceptions.TinkoffDataServiceException"></exception>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>all market instrument</returns>
        Task<IEnumerable<MarketInstrument>> GetAllStoks(CancellationToken cancellationToken);

        /// <summary>
        /// return info about candle on figi stock
        /// </summary>
        /// <param name="figi">figi stock</param>
        /// <param name="from">from DataTime</param>
        /// <param name="to">to DataTime</param>
        /// <param name="interval">interval cadndle</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <exception cref="Exceptions.TinkoffDataServiceException"></exception>
        /// <returns>candle stock</returns>
        Task<IEnumerable<CandlePayload>> GetStockCandles(string figi, DateTime from, DateTime to, CandleInterval interval, CancellationToken cancellationToken);
    }
}
