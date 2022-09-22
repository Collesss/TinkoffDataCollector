using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinkoffDataCollector.TinkoffDataCollectorService.Exceptions
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
