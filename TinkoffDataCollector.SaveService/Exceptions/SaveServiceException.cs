namespace TinkoffDataCollector.SaveService.Exceptions
{
    public class SaveServiceException : Exception
    {
        public SaveServiceException()
        {

        }

        public SaveServiceException(string message) : base(message)
        {

        }

        public SaveServiceException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
