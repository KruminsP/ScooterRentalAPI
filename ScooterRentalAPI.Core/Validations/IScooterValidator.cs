using ScooterRentalAPI.Core.Models;

namespace ScooterRentalAPI.Core.Validations
{
    public interface IScooterValidator
    {
        public bool IsValid(Scooter scooter);
    }
}