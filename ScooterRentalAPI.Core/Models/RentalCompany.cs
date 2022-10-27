using System;
using System.Collections.Generic;
using System.Linq;
using ScooterRentalAPI.Interfaces;

namespace ScooterRentalAPI.Core.Models
{
    public class RentalCompany : IRentalCompany
    {
        public string Name { get; }
        private static List<RentedScooter> _rentedScooters = new List<RentedScooter>();
        private static Calculators _feeCalculator = new Calculators();

        public RentalCompany(string name)
        {
            Name = name;
        }

        public static RentedScooter StartRent(int id)
        {
            var scooter = ScooterService.GetScooterById(id);

            //if (scooter.IsRented)
            //{
            //    return ScooterAlreadyRentedException();
            //}

            scooter.IsRented = true;
            var rentedScooter = new RentedScooter("a",DateTime.UtcNow, scooter.PricePerMinute);
            _rentedScooters.Add(rentedScooter);

            return rentedScooter;
        }

        public static decimal EndRent(int id)
        {
            var scooter = ScooterService.GetScooterById(id);
            var rentedScooter = _rentedScooters.FirstOrDefault(s => s.Id == id && !s.EndTime.HasValue);
            
            rentedScooter.EndTime = DateTime.UtcNow;
            
            var scooterFee = _feeCalculator.ScooterFeeCalculator(rentedScooter.StartTime,
                (DateTime)rentedScooter.EndTime, rentedScooter.PricePerMinute);

            scooter.IsRented = false;

            return scooterFee;
        }

        public static List<RentedScooter> GetAllRentedScooters()
        {
            return _rentedScooters;
        }

        public static decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            decimal income = 0;

            if (year == null)
            {
                foreach (var scooter in _rentedScooters)
                {
                    if (scooter.EndTime == null)
                    {
                        if(includeNotCompletedRentals)
                        {
                            income += _feeCalculator.ScooterFeeCalculator(scooter.StartTime,
                                DateTime.UtcNow,
                                scooter.PricePerMinute);
                        }
                    }
                    else
                    {
                        income += _feeCalculator.ScooterFeeCalculator(scooter.StartTime,
                            (DateTime)scooter.EndTime,
                            scooter.PricePerMinute);
                    }
                }
            }
            else // valid year
            {
                foreach (var scooter in _rentedScooters)
                {
                    if (scooter.EndTime == null)
                    {
                        if (includeNotCompletedRentals)
                        {
                            income += _feeCalculator.ScooterFeeCalculator(scooter.StartTime,
                                DateTime.UtcNow,
                                scooter.PricePerMinute);
                        }
                    }
                    else if (((DateTime)scooter.EndTime).Year == year)
                    {
                        income += _feeCalculator.ScooterFeeCalculator(scooter.StartTime,
                            (DateTime)scooter.EndTime,
                            scooter.PricePerMinute);
                    }
                }
            }

            return income;
        }

        public static bool IsRented(Scooter scooter)
        {
            return scooter.IsRented;
        }
    }
}
