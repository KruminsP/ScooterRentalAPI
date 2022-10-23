using System;
using System.Collections.Generic;
using System.Linq;
using ScooterRental.Exceptions;
using ScooterRental.Interfaces;

namespace ScooterRental
{
    public class RentalCompany : IRentalCompany
    {
        public string Name { get; }
        private IScooterService _service;
        private List<RentedScooters> _rentedScooters;
        private Calculators _feeCalculator;

        public RentalCompany(string name, IScooterService service, List<RentedScooters> rentedScooters)
        {
            Name = name;
            _service = service;
            _rentedScooters = rentedScooters;
            _feeCalculator = new Calculators();
        }

        public void StartRent(string id)
        {
            var scooter = _service.GetScooterById(id);

            if (scooter.IsRented)
            {
                throw new ScooterAlreadyRentedException(id);
            }

            scooter.IsRented = true;
            _rentedScooters.Add(new RentedScooters(scooter.Id, DateTime.UtcNow, scooter.PricePerMinute));
        }

        public decimal EndRent(string id)
        {
            var scooter = _service.GetScooterById(id);
            var rentedScooter = _rentedScooters.FirstOrDefault(s => s.Id == id && !s.EndTime.HasValue);
            
            rentedScooter.EndTime = DateTime.UtcNow;
            
            var scooterFee = _feeCalculator.ScooterFeeCalculator(rentedScooter.StartTime, (DateTime)rentedScooter.EndTime, rentedScooter.PricePerMinute);

            scooter.IsRented = false;

            return scooterFee;
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
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
                    else if(((DateTime)scooter.EndTime).Year == year)
                    {
                        income += _feeCalculator.ScooterFeeCalculator(scooter.StartTime,
                            (DateTime)scooter.EndTime,
                            scooter.PricePerMinute);
                    }
                }
            }

            return income;
        }
    }
}
