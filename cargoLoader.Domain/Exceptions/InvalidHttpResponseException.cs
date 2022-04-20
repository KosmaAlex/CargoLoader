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
        public InvalidHttpResponseException(string uri)
        {
            Uri = uri;
        }

        public InvalidHttpResponseException(string uri, string? message) : base(message)
        {
            Uri = uri;
        }

        public InvalidHttpResponseException(string uri, string? message, Exception? innerException) : base(message, innerException)
        {
            Uri = uri;
        }
    }
}
