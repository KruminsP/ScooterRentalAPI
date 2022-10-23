using System.Runtime.InteropServices.ComTypes;

namespace ScooterRentalAPI
{
    public class Scooter
    {
        public Scooter(decimal pricePerMinute)
        {
            PricePerMinute = pricePerMinute;
            IsRented = false;
        }
        public int Id { get; set; }
        public decimal PricePerMinute { get; }
        public bool IsRented { get; set; }
    }
}
