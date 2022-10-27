using ScooterRentalAPI.Core.Models;

namespace ScooterRentalAPI.Core.Services
{
    public interface IScooterService : IEntityService<Scooter>
    {
        void ChangeScooterRentedStatus(string name);
    }
}