namespace TinkoffDataCollector.TinkoffDataCollector.Exceptions
{
    public class TinkoffDataCollectorException : Exception
    {
        public TinkoffDataCollectorException()
        {

        }

        public TinkoffDataCollectorException(string message) : base(message)
        {

        }

        public TinkoffDataCollectorException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
