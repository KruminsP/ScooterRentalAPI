using Microsoft.AspNetCore.Mvc;
using ScooterRentalAPI.Core.Models;

namespace ScooterRentalAPI.Controllers
{
    [Microsoft.AspNetCore.Components.Route("admin-api")]
    [ApiController]
    public class ScooterServiceController : ControllerBase
    {
        [Route("scooters")]
        [HttpPost]
        public IActionResult PostScooter(Scooter scooter)
        {
            if (ScooterService.ScooterExists(scooter))
            {
                return Conflict();
            }

            if (scooter.PricePerMinute < 0)
            {
                return BadRequest("Price cannot be negative");
            }

            ScooterService.AddScooter(scooter);
            return Created("", scooter);
        }

        [Route("scooters/{id}")]
        [HttpGet]
        public IActionResult GetScooter(int id)
        {
            var scooter = ScooterService.GetScooterById(id);

            if (scooter == null)
            {
                return NotFound();
            }

            return Ok(scooter);
        }

        [Route("scooters/{id}")]
        [HttpDelete]
        public IActionResult DeleteScooter(int id)
        {
            ScooterService.RemoveScooter(id);

            return Ok();
        }

        [Route("scooters")]
        [HttpGet]
        public IActionResult GetAllScooters()
        {
            var scooters = ScooterService.GetScooters();
            return Ok(scooters);
        }
    }
}