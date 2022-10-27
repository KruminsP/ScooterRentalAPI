using ScooterRentalAPI.Core.Models;

namespace ScooterRentalAPI.Core.Validations
{
    public class ScooterValidator : IScooterValidator
    {
        public bool IsValid(Scooter scooter)
        {
            return scooter != null;
        }
    }
}