using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Exceptions
{
    public class OrderAlreadyExistException : Exception
    {
        public int OrderId { get; }
        public string OrderNumber { get; }


        public OrderAlreadyExistException(int orderId, string orderNumber)
        {
            OrderId = orderId;
            OrderNumber = orderNumber;
        }

        public OrderAlreadyExistException(int orderId, string orderNumber, string? message) : base(message)
        {
            OrderId = orderId;
            OrderNumber = orderNumber;
        }

        public OrderAlreadyExistException(int orderId, string orderNumber, string? message, Exception? innerException) : base(message, innerException)
        {
            OrderId = orderId;
            OrderNumber = orderNumber;
        }
    }
}
