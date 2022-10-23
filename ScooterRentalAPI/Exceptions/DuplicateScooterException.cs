using System;

namespace ScooterRentalAPI.Exceptions
{
    public class DuplicateScooterException : Exception
    {
        public DuplicateScooterException(string id) :
            base($"Scooter with id {id} already exists") { }
    }
}
