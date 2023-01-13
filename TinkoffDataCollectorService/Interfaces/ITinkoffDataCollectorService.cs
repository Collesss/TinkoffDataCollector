namespace TinkoffDataCollectorService.Interfaces
{
    public interface ITinkoffDataCollectorService
    {
        /// <summary>
        /// Service save data about candles
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="Exceptions.TinkoffDataCollectorServiceException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        /// <returns>Task</returns>
        Task Run(CancellationToken cancellationToken);
    }
}
