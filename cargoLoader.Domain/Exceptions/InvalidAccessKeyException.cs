using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Exceptions
{
    public class InvalidAccessKeyException : Exception
    {
        public string Key { get; set; }
        public string Api { get; set; }

        public InvalidAccessKeyException(string key, string api)
        {
            Key = key;
            Api = api;
        }

        public InvalidAccessKeyException(string key, string api, string? message) : base(message)
        {
            Key = key;
            Api = api;
        }

        public InvalidAccessKeyException(string key, string api, string? message, Exception? innerException) : base(message, innerException)
        {
            Key = key;
            Api = api;
        }
    }
}
