namespace TinkoffDataCollectorService.Exceptions
{
    public class TinkoffDataCollectorServiceException : Exception
    {
        public TinkoffDataCollectorServiceException()
        {

        }

        public TinkoffDataCollectorServiceException(string message) : base(message)
        {

        }

        public TinkoffDataCollectorServiceException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
