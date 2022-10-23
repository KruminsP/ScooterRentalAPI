using System;

namespace ScooterRental
{
    public class Calculators
    {
        public decimal ScooterFeeCalculator(DateTime start, DateTime end, decimal pricePerMinute)
        {
            var spanInDays = (decimal)(end - start).TotalDays;

            decimal distinctDates = end.TimeOfDay > start.TimeOfDay ? Math.Ceiling(spanInDays) : Math.Ceiling(spanInDays) + 1;
            
            var lastDayFromMidnightMinutes = end.TimeOfDay.TotalMinutes;
            var firstDayToMidnightMinutes = (1440 - start.TimeOfDay.TotalMinutes);

            var singleDayCost = (decimal)Math.Ceiling((end - start).TotalMinutes) * pricePerMinute;

            var firstDayCost = Math.Min(20, (decimal)Math.Ceiling(firstDayToMidnightMinutes) * pricePerMinute);
            var lastDayCost = Math.Min(20, (decimal)Math.Ceiling(lastDayFromMidnightMinutes) * pricePerMinute);
            var middleDayCost = Math.Min(20 * (distinctDates - 2), (1440 * pricePerMinute) * (distinctDates - 2));

            decimal totalCost;

            switch (distinctDates)
            {
                case 1:
                    totalCost = singleDayCost;
                    break;
                case 2:
                    totalCost = firstDayCost + lastDayCost;
                    break;
                default:
                    totalCost = firstDayCost + lastDayCost + middleDayCost;
                    break;
            }

            return totalCost;
        }
    }
}
