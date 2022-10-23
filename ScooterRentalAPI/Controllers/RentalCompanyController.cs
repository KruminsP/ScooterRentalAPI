using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using ScooterRentalAPI;

namespace FlightPlanner.Controllers
{
    [Microsoft.AspNetCore.Components.Route("admin-api")]
    [ApiController]
    public class RentalCompanyController : ControllerBase
    {
        [Route("rental/{id}")]
        [HttpPost]
        public IActionResult StartRent(int id)
        {
            if (ScooterService.GetScooterById(id)==null)
            {
                return NotFound("No such scooter");
            }

            if (ScooterService.GetScooterById(id).IsRented)
            {
                return Conflict("Scooter already rented");
            }

            var scooter = RentalCompany.StartRent(id);

            return Ok(scooter);
        }

        [Route("rental/{id}")]
        [HttpPut]
        public IActionResult EndRent(int id)
        {
            if (ScooterService.GetScooterById(id) == null)
            {
                return NotFound("No such scooter");
            }

            if (!ScooterService.GetScooterById(id).IsRented)
            {
                return Conflict("Scooter is not rented out");
            }

            var fee = RentalCompany.EndRent(id);


            return Ok(fee);
        }

        [Route("rental")]
        [HttpGet]
        public IActionResult GetAllRentedScooters()
        {
            return Ok(RentalCompany.GetAllRentedScooters());
        }

        [Route("income")]
        [HttpGet]
        public IActionResult CalculateIncome(int year, bool includeRented)
        {
            var income = RentalCompany.CalculateIncome(year, includeRented);

            return Ok(income);
        }
    }
}