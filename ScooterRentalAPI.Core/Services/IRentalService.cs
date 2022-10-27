using ScooterRentalAPI.Core.Models;

namespace ScooterRentalAPI.Core.Services
{
    public interface IRentalService : IEntityService<RentedScooter>
    {
        RentedScooter GetCurrentlyRentedScooter(string name);
    }
}