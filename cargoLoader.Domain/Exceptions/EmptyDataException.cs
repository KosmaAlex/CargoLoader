using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Exceptions
{
    public class EmptyDataException : Exception
    {
        public string URL { get; }

        public EmptyDataException(string url)
        {
            URL = url;
        }

        public EmptyDataException(string url, string? message) : base(message)
        {
            URL = url;
        }

        public EmptyDataException(string url, string? message, Exception? innerException) : base(message, innerException)
        {
            URL = url;
        }
    }
}
