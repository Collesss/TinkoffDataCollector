using System;
using System.Net;

namespace TinkoffDataCollector.TinkoffDataService.Implementation.GetDataCacheHttpClient.TinkoffHttpClient.Exceptions
{
    public class TinkoffHttpClientException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }

        public string TrakingId { get; }

        
        public TinkoffHttpClientException(string message, string code, string trackingId, HttpStatusCode httpStatusCode) 
            : base($"{code}: {message} ({trackingId}).")
        {
            HttpStatusCode = httpStatusCode;
            TrakingId = trackingId;
        }

        public TinkoffHttpClientException(string message, string code, string trackingId, HttpStatusCode httpStatusCode, Exception innerException) 
            : base($"{code}: {message} ({trackingId}).", innerException)
        {
            HttpStatusCode = httpStatusCode;
            TrakingId = trackingId;
        }
    }
}
