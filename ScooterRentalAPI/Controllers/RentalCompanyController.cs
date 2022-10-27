using Microsoft.AspNetCore.Mvc;
using ScooterRentalAPI.Core.Models;
using ScooterRentalAPI.Data;
using System;
using System.Linq;

namespace ScooterRentalAPI.Controllers
{
    [Microsoft.AspNetCore.Components.Route("admin-api")]
    [ApiController]
    public class RentalCompanyController : ControllerBase
    {
        private readonly ScooterRentalDbContext _context;
        private readonly Calculators _calculator;

        public RentalCompanyController(ScooterRentalDbContext context, Calculators calculator)
        {
            _context = context;
            _calculator = calculator;
        }

        [Route("rental/{name}")]
        [HttpPost]
        public IActionResult StartRent(string name)
        {
            var scooter = _context.Scooters.FirstOrDefault(s => s.Name == name);
            if (scooter == null)
            {
                return NotFound("No such scooter");
            }

            if (scooter.IsRented)
            {
                return Conflict("Scooter already rented");
            }

            scooter.IsRented = true;
            var rentedScooter = new RentedScooter(name, DateTime.UtcNow, scooter.PricePerMinute);
            _context.RentedScooters.Add(rentedScooter);
            _context.SaveChanges();

            return Created("", rentedScooter);
        }

        [Route("rental/{name}")]
        [HttpPut]
        public IActionResult EndRent(string name)
        {
            var scooter = _context.Scooters.FirstOrDefault(s => s.Name == name);

            if (scooter == null)
            {
                return NotFound("No such scooter");
            }

            if (!scooter.IsRented)
            {
                return Conflict("Scooter is not rented out");
            }
            
            var rentedScooter = _context.RentedScooters.FirstOrDefault(s => s.Name == name);// && !s.EndTime.HasValue);
            if (rentedScooter == null)
            {
                return Conflict("No such scooter");
            }

            rentedScooter.EndTime = DateTime.UtcNow;
            var scooterFee = _calculator.ScooterFeeCalculator(rentedScooter.StartTime,
                (DateTime)rentedScooter.EndTime, rentedScooter.PricePerMinute);
            scooter.IsRented = false;
            _context.SaveChanges();

            return Ok(scooterFee);
        }

        [Route("rental")]
        [HttpGet]
        public IActionResult GetAllRentedScooters()
        {
            return Ok(_context.RentedScooters);
        }

        [Route("rental/income")]
        [HttpGet]
        public IActionResult CalculateIncome(IncomeRequest request)
        {
            decimal income = 0;

            if (request.Year == null)
            {
                foreach (var scooter in _context.RentedScooters)
                {
                    if (scooter.EndTime == null)
                    {
                        if (request.IncludeRented)
                        {
                            income += _calculator.ScooterFeeCalculator(scooter.StartTime,
                                DateTime.UtcNow,
                                scooter.PricePerMinute);
                        }
                    }
                    else
                    {
                        income += _calculator.ScooterFeeCalculator(scooter.StartTime,
                            (DateTime)scooter.EndTime,
                            scooter.PricePerMinute);
                    }
                }
            }
            else // valid year
            {
                foreach (var scooter in _context.RentedScooters)
                {
                    if (scooter.EndTime == null)
                    {
                        if (request.IncludeRented)
                        {
                            income += _calculator.ScooterFeeCalculator(scooter.StartTime,
                                DateTime.UtcNow,
                                scooter.PricePerMinute);
                        }
                    }
                    else if (((DateTime)scooter.EndTime).Year == request.Year)
                    {
                        income += _calculator.ScooterFeeCalculator(scooter.StartTime,
                            (DateTime)scooter.EndTime,
                            scooter.PricePerMinute);
                    }
                }
            }

            _context.SaveChanges();

            return Ok(income);
        }
    }
}
