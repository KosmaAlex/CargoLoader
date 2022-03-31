using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Exceptions
{
    public class EntityDoesNotExistException : Exception
    {
        public string EntityName { get; }
        public string RequestedParameter { get; }
        public string ParameterValue { get; }

        public EntityDoesNotExistException(string entityName, string requestedParameter, string parameterValue)
        {
            EntityName = entityName;
            RequestedParameter = requestedParameter;
            ParameterValue = parameterValue;
        }

        public EntityDoesNotExistException(string entityName, string requestedParameter, string parameterValue, string? message) : base(message)
        {
            EntityName = entityName;
            RequestedParameter = requestedParameter;
            ParameterValue = parameterValue;
        }

        public EntityDoesNotExistException(string entityName, string requestedParameter, string parameterValue, string? message, Exception? innerException) : base(message, innerException)
        {
            EntityName = entityName;
            RequestedParameter = requestedParameter;
            ParameterValue = parameterValue;
        }

    }
}
