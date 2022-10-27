using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ScooterRentalAPI.Core.Models;
using ScooterRentalAPI.Core.Services;

namespace ScooterRentalAPI.Controllers
{
    [Microsoft.AspNetCore.Components.Route("scooters")]
    [ApiController]
    public class ScooterServiceController : ControllerBase
    {
        private readonly IScooterService _scooterService;

        public ScooterServiceController(IScooterService scooterService)
        {
            _scooterService = scooterService;
        }

        [Route("scooters")]
        [HttpPost]
        public IActionResult PostScooter(Scooter scooter)
        {
            if (_scooterService.GetAll().Any(s => s.Name == scooter.Name))
            {
                return BadRequest("Name should be unique");
            }

            if (scooter.PricePerMinute < 0)
            {
                return BadRequest("Price cannot be negative");
            }

            _scooterService.Create(scooter);

            return Created("", scooter);
        }

        [Route("scooters/{name}")]
        [HttpGet]
        public IActionResult GetScooter(string name)
        {
            var scooter = _scooterService.GetByName(name);
            if (scooter == null)
            {
                return NotFound();
            }

            return Ok(scooter);
        }

        [Route("scooters/{name}")]
        [HttpDelete]
        public IActionResult DeleteScooter(string name)
        {
            var scooter = _scooterService.GetByName(name);
            if (scooter != null)
            {
                _scooterService.Delete(scooter);
            }

            return Ok();
        }

        [Route("scooters")]
        [HttpGet]
        public IActionResult GetAllScooters()
        {
            var scooters = _scooterService.GetAll();
            return Ok(scooters);
        }
    }
}