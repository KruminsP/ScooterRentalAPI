using System;

namespace ScooterRentalAPI
{
    public class RentedScooters
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal PricePerMinute { get; set; }

        public RentedScooters(int id, DateTime startTime, decimal pricePerMinute)
        {
            Id = id;
            StartTime = startTime;
            PricePerMinute = pricePerMinute;
        }
    }
}
