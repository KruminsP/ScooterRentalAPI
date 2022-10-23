using System;

namespace ScooterRentalAPI.Exceptions
{
    public class InvalidPriceException : Exception
    {
        public InvalidPriceException(decimal price) :
            base($"Given price {price} not valid") { }
    }
}
