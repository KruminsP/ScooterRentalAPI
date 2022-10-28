using System;
using System.Collections.Generic;

namespace ScooterRentalAPI.Core.Models
{
    public class Calculator
    {
        public decimal CalculateIncome(IncomeRequest request, List<RentedScooter> scooters)
        {
            decimal income = 0;

            if (request.Year == null)
            {
                foreach (var scooter in scooters)
                {
                    if (scooter.EndTime == null)
                    {
                        if (request.IncludeRented)
                        {
                            income += ScooterFeeCalculator(scooter.StartTime,
                                DateTime.UtcNow,
                                scooter.PricePerMinute);
                        }
                    }
                    else
                    {
                        income += ScooterFeeCalculator(scooter.StartTime,
                            (DateTime)scooter.EndTime,
                            scooter.PricePerMinute);
                    }
                }
            }
            else // valid year
            {
                foreach (var scooter in scooters)
                {
                    if (scooter.EndTime == null)
                    {
                        if (request.IncludeRented)
                        {
                            income += ScooterFeeCalculator(scooter.StartTime,
                                DateTime.UtcNow,
                                scooter.PricePerMinute);
                        }
                    }
                    else if (((DateTime)scooter.EndTime).Year == request.Year)
                    {
                        income += ScooterFeeCalculator(scooter.StartTime,
                            (DateTime)scooter.EndTime,
                            scooter.PricePerMinute);
                    }
                }
            }

            return income;
        }
        public decimal ScooterFeeCalculator(DateTime start, DateTime end, decimal pricePerMinute)
        {
            var spanInDays = (decimal)(end - start).TotalDays;

            decimal distinctDates = end.TimeOfDay > start.TimeOfDay ? Math.Ceiling(spanInDays) : Math.Ceiling(spanInDays) + 1;

            var lastDayFromMidnightMinutes = end.TimeOfDay.TotalMinutes;
            var firstDayToMidnightMinutes = 1440 - start.TimeOfDay.TotalMinutes;

            var singleDayCost = (decimal)Math.Ceiling((end - start).TotalMinutes) * pricePerMinute;

            var firstDayCost = Math.Min(20, (decimal)Math.Ceiling(firstDayToMidnightMinutes) * pricePerMinute);
            var lastDayCost = Math.Min(20, (decimal)Math.Ceiling(lastDayFromMidnightMinutes) * pricePerMinute);
            var middleDayCost = Math.Min(20 * (distinctDates - 2), 1440 * pricePerMinute * (distinctDates - 2));

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
