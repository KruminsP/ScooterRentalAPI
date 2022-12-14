using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using ScooterRentalAPI;

namespace FlightPlanner.Controllers
{
    [Microsoft.AspNetCore.Components.Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        [Route("scooters")]
        [HttpPost]
        public IActionResult PostScooter(Scooter scooter)
        {
            if (ScooterService.ScooterExists(scooter))
            {
                return Conflict();
            }

            ScooterService.AddScooter(scooter);
            return Created("", scooter);
        }

        [Route("scooters/{id}")]
        [HttpGet]
        public IActionResult GetScooter(string id)
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
        public IActionResult DeleteScooter(string id)
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