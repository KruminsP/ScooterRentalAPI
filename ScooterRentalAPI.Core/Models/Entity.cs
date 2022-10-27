using ScooterRentalAPI.Interfaces;

namespace ScooterRentalAPI.Core.Models
{
    public class Entity : IEntity
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}