using System.Runtime.InteropServices.ComTypes;

namespace ScooterRentalAPI.Core.Models
{
    public class Scooter
    {
        public Scooter(string name, decimal pricePerMinute)
        {
            Name = name;
            PricePerMinute = pricePerMinute;
            IsRented = false;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PricePerMinute { get; set; }
        public bool IsRented { get; set; }
    }
}
