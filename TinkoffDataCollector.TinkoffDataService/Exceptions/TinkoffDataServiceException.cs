using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TinkoffDataCollector.TinkoffDataService.Exceptions
{
    public class TinkoffDataServiceException : Exception
    {
        public TinkoffDataServiceException()
        {

        }
        
        public TinkoffDataServiceException(string message) : base(message)
        {

        }

        public TinkoffDataServiceException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
