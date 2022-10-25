using System;

namespace ScooterRentalAPI.Exceptions
{
    public class DuplicateScooterException : Exception
    {
        public DuplicateScooterException(int id) :
            base($"Scooter with id {id} already exists") { }
    }
}
