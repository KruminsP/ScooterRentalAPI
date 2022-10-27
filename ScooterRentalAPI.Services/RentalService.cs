using ScooterRentalAPI.Core.Models;
using ScooterRentalAPI.Core.Services;
using ScooterRentalAPI.Data;
using System.Linq;

namespace ScooterRentalAPI.Services
{
    public class RentalService : EntityService<RentedScooter>, IRentalService
    {
        public RentalService(IScooterRentalDbContext context) : base(context)
        {
        }

        public RentedScooter GetCurrentlyRentedScooter(string name)
        {
            var rentedScooter = _context.RentedScooters
                .FirstOrDefault(r => r.Name == name && r.EndTime == null);

            return rentedScooter;
        }
    }
}