using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ScooterRentalAPI.Core.Models;
using ScooterRentalAPI.Data;

namespace ScooterRentalAPI.Controllers
{
    [Microsoft.AspNetCore.Components.Route("admin-api")]
    [ApiController]
    public class ScooterServiceController : ControllerBase
    {
        private readonly ScooterRentalDbContext _context;

        public ScooterServiceController(ScooterRentalDbContext context)
        {
            _context = context;
        }

        [Route("scooters")]
        [HttpPost]
        public IActionResult PostScooter(Scooter scooter)
        {
            if (_context.Scooters.Any(s => s.Name == scooter.Name))
            {
                return BadRequest("Name should be unique");
            }

            if (scooter.PricePerMinute < 0)
            {
                return BadRequest("Price cannot be negative");
            }

            _context.Scooters.Add(scooter);
            _context.SaveChanges();

            return Created("", scooter);
        }

        [Route("scooters/{name}")]
        [HttpGet]
        public IActionResult GetScooter(string name)
        {
            var scooter = _context.Scooters.FirstOrDefault(s => s.Name == name);
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
            var scooter = _context.Scooters.FirstOrDefault(s => s.Name == name);
            if (scooter != null)
            {
                _context.Scooters.Remove(scooter);
                _context.SaveChanges();
            }

            return Ok();
        }

        [Route("scooters")]
        [HttpGet]
        public IActionResult GetAllScooters()
        {
            var scooters = _context.Scooters;
            return Ok(scooters);
        }
    }
}