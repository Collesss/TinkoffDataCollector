namespace TinkoffDataCollector.TinkoffDataCollector.Interfaces
{
    internal interface ITinkoffDataCollector
    {
        Task Run(CancellationToken cancellationToken);
    }
}
