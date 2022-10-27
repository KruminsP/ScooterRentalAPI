using Microsoft.AspNetCore.Mvc;
using ScooterRentalAPI.Core.Models;
using ScooterRentalAPI.Core.Services;
using System;

namespace ScooterRentalAPI.Controllers
{
    [Microsoft.AspNetCore.Components.Route("rental")]
    [ApiController]
    public class RentalCompanyController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly IScooterService _scooterService;
        private readonly Calculators _calculator;

        public RentalCompanyController(
            IRentalService rentalService,
            IScooterService scooterService,
            Calculators calculator)
        {
            _rentalService = rentalService;
            _scooterService = scooterService;
            _calculator = calculator;
        }

        [Route("rental/{name}")]
        [HttpPost]
        public IActionResult StartRent(string name)
        {
            var scooter = _scooterService.GetByName(name);

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
            _rentalService.Create(rentedScooter);

            return Created("", rentedScooter);
        }

        [Route("rental/{name}")]
        [HttpPut]
        public IActionResult EndRent(string name)
        {
            var scooter = _scooterService.GetByName(name);
            if (scooter == null)
            {
                return NotFound("No such scooter");
            }

            if (!scooter.IsRented)
            {
                return Conflict("Scooter is not rented out");
            }

            var rentedScooter = _rentalService.GetCurrentlyRentedScooter(name);
            if (rentedScooter == null)
            {
                return Conflict("No such scooter");
            }

            rentedScooter.EndTime = DateTime.UtcNow;
            _scooterService.ChangeScooterRentedStatus(name);

            var scooterFee = _calculator.ScooterFeeCalculator(rentedScooter.StartTime,
                (DateTime)rentedScooter.EndTime, rentedScooter.PricePerMinute);

            return Ok(scooterFee);
        }

        [Route("rental")]
        [HttpGet]
        public IActionResult GetAllRentedScooters()
        {
            return Ok(_rentalService.GetAll());
        }

        [Route("rental/income")]
        [HttpGet]
        public IActionResult CalculateIncome(IncomeRequest request)
        {
            decimal income = 0;

            if (request.Year == null)
            {
                foreach (var scooter in _rentalService.GetAll())
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
                foreach (var scooter in _rentalService.GetAll())
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

            return Ok(income);
        }
    }
}
