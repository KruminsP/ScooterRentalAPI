using ScooterRentalAPI.Core.Models;
using ScooterRentalAPI.Exceptions;

namespace ScooterRentalAPI.Core.Validators
{
    public static class Validator
    {
        public static void ScooterIdValidator(int id)
        {
            if (id==null)
            {
                throw new InvalidIdException();
            }
        }

        public static void ScooterExistingValidator(Scooter scooter, string id)
        {
            if (scooter == null)
            {
                throw new ScooterDoesNotExistException(id);
            }
        }
    }
}
