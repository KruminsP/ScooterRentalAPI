namespace ScooterRentalAPI.Core.Models
{
    public class Scooter : Entity
    {
        public Scooter(string name, decimal pricePerMinute)
        {
            Name = name;
            PricePerMinute = pricePerMinute;
            IsRented = false;
        }
        public decimal PricePerMinute { get; set; }
        public bool IsRented { get; set; }
    }
}
