using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Exceptions
{
    public class InvalidHttpResponseException : Exception
    {
        public string Uri { get; set; }
        public string StatusCode { get; set; }
        public InvalidHttpResponseException(string uri, string statusCode)
        {
            Uri = uri;
            StatusCode = statusCode;
        }

        public InvalidHttpResponseException(string uri, string statusCode, string? message) : base(message)
        {
            Uri = uri;
            StatusCode = statusCode;
        }

        public InvalidHttpResponseException(string uri, string statusCode, string? message, Exception? innerException) : base(message, innerException)
        {
            Uri = uri;
            StatusCode = statusCode;
        }
    }
}
