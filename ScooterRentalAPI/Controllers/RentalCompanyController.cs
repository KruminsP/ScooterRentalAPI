using Microsoft.AspNetCore.Mvc;
using ScooterRentalAPI.Core.Models;

namespace ScooterRentalAPI.Controllers
{
    [Microsoft.AspNetCore.Components.Route("admin-api")]
    [ApiController]
    public class RentalCompanyController : ControllerBase
    {
        [Route("rental/{id}")]
        [HttpPost]
        public IActionResult StartRent(int id)
        {
            if (ScooterService.GetScooterById(id) == null)
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

        [Route("rental/income")]
        [HttpGet]
        public IActionResult CalculateIncome(IncomeRequest request)
        {
            var income = RentalCompany.CalculateIncome(request.Year, request.IncludeRented);

            return Ok(income);
        }
    }
}