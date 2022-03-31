using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public string RequestedParameter { get; }

        public ItemNotFoundException(string requestedParametr)
        {
            RequestedParameter = requestedParametr;
        }

        public ItemNotFoundException(string requestedParametr, string? message) : base(message)
        {
            RequestedParameter = requestedParametr;
        }

        public ItemNotFoundException(string requestedParametr, string? message, Exception? innerException) : base(message, innerException)
        {
            RequestedParameter = requestedParametr;
        }
    }
}
