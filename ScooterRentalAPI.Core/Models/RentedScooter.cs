using System;

namespace ScooterRentalAPI.Core.Models
{
    public class RentedScooter
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal PricePerMinute { get; set; }

        public RentedScooter(int id, DateTime startTime, decimal pricePerMinute)
        {
            Id = id;
            StartTime = startTime;
            PricePerMinute = pricePerMinute;
        }
    }
}
