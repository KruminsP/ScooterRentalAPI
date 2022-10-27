using ScooterRentalAPI.Core.Models;
using ScooterRentalAPI.Core.Services;
using ScooterRentalAPI.Data;
using System.Linq;

namespace ScooterRentalAPI.Services
{
    public class ScooterService : EntityService<Scooter>, IScooterService
    {
        public ScooterService(IScooterRentalDbContext context) : base(context)
        {
        }

        public void ChangeScooterRentedStatus(string name)
        {
            var scooter = _context.Scooters.FirstOrDefault(s => s.Name == name);
            scooter.IsRented = !scooter.IsRented;
            _context.SaveChanges();
        }
    }
}