using System.Net;

namespace DataService.GetDataCacheHttpClient.HttpClient.Exceptions
{
    public class HttpClientException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }

        public string TrakingId { get; }


        public HttpClientException(string message, string code, string trackingId, HttpStatusCode httpStatusCode)
            : base($"{code}: {message} ({trackingId}).")
        {
            HttpStatusCode = httpStatusCode;
            TrakingId = trackingId;
        }

        public HttpClientException(string message, string code, string trackingId, HttpStatusCode httpStatusCode, Exception innerException)
            : base($"{code}: {message} ({trackingId}).", innerException)
        {
            HttpStatusCode = httpStatusCode;
            TrakingId = trackingId;
        }
    }
}
