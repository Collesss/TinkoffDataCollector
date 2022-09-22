namespace TinkoffDataCollector.TinkoffDataCollectorService.Interfaces
{
    public interface ITinkoffDataCollectorService
    {
        Task Run(CancellationToken cancellationToken);
    }
}
