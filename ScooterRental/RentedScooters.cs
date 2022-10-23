using System;

namespace ScooterRental
{
    public class RentedScooters
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal PricePerMinute { get; set; }

        public RentedScooters(string id, DateTime startTime, decimal pricePerMinute)
        {
            Id = id;
            StartTime = startTime;
            PricePerMinute = pricePerMinute;
        }
    }
}
