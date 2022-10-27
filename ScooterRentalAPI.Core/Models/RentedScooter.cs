using System;

namespace ScooterRentalAPI.Core.Models
{
    public class RentedScooter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal PricePerMinute { get; set; }

        public RentedScooter(string name, DateTime startTime, decimal pricePerMinute)
        {
            Name = name;
            StartTime = startTime;
            PricePerMinute = pricePerMinute;
        }
    }
}
