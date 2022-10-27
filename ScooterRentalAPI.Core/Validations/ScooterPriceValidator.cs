using ScooterRentalAPI.Core.Models;

namespace ScooterRentalAPI.Core.Validations
{
    public class ScooterPriceValidator : IScooterValidator
    {
        public bool IsValid(Scooter scooter)
        {
            return scooter.PricePerMinute >= 0;
        }
    }
}