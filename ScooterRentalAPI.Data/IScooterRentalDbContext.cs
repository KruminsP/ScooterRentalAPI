using Microsoft.EntityFrameworkCore;
using ScooterRentalAPI.Core.Models;

namespace ScooterRentalAPI.Data
{
    public interface IScooterRentalDbContext
    {
        DbSet<T> Set<T>() where T : class;
        DbSet<Scooter> Scooters { get; set; }
        DbSet<RentedScooter> RentedScooters { get; set; }
        int SaveChanges();
    }
}