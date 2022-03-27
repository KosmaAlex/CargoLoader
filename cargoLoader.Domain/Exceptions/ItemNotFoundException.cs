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
        public string RequestedParametr { get; }

        public ItemNotFoundException(string requestedParametr)
        {
            RequestedParametr = requestedParametr;
        }

        public ItemNotFoundException(string requestedParametr, string? message) : base(message)
        {
            RequestedParametr = requestedParametr;
        }

        public ItemNotFoundException(string requestedParametr, string? message, Exception? innerException) : base(message, innerException)
        {
            RequestedParametr = requestedParametr;
        }
    }
}
