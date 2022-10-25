using System;

namespace ScooterRentalAPI.Exceptions
{
    public class ScooterAlreadyRentedException : Exception
    {
        public ScooterAlreadyRentedException(string id) :
            base($"Scooter with ID {id} already rented") { }
    }
}
