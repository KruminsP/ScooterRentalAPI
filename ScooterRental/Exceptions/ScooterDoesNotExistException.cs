using System;

namespace ScooterRental.Exceptions
{
    public class ScooterDoesNotExistException : Exception
    {
        public ScooterDoesNotExistException(string id) :
            base($"Scooter {id} does not exist") { }
    }
}
