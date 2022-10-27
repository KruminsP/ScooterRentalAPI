using Microsoft.EntityFrameworkCore;
using ScooterRentalAPI.Core.Models;

namespace ScooterRentalAPI.Data
{
    public class ScooterRentalDbContext : DbContext, IScooterRentalDbContext
    {
        public ScooterRentalDbContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Scooter> Scooters { get; set; }
        public DbSet<RentedScooter> RentedScooters { get; set; }
    }
}