using ScooterRentalAPI.Core.Models;

namespace ScooterRentalAPI.Core.Validations
{
    public class ScooterNameValidator : IScooterValidator
    {
        public bool IsValid(Scooter scooter)
        {
            return !string.IsNullOrEmpty(scooter?.Name);
        }
    }
}