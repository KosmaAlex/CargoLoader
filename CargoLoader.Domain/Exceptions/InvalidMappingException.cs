using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Exceptions
{
    public class InvalidMappingException : Exception
    {
        public string Source { get; set; }
        public Type Type { get; set; }

        public InvalidMappingException(string source, Type type)
        {
            Source = source;
            Type = type;
        }
        public InvalidMappingException(string source, Type type, string? message) : base(message)
        {
            Source = source;
            Type = type;
        }

        public InvalidMappingException(string source, Type type, string? message, Exception? innerException) : base(message, innerException)
        {
            Source = source;
            Type = type;
        }

        
    }
}
