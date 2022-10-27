using Microsoft.AspNetCore.Mvc;
using ScooterRentalAPI.Core.Models;
using ScooterRentalAPI.Core.Services;
using ScooterRentalAPI.Core.Validations;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRentalAPI.Controllers
{
    [Microsoft.AspNetCore.Components.Route("scooters")]
    [ApiController]
    public class ScooterServiceController : ControllerBase
    {
        private readonly IScooterService _scooterService;
        private readonly IEnumerable<IScooterValidator> _scooterValidators;

        public ScooterServiceController(
            IScooterService scooterService,
            IEnumerable<IScooterValidator> scooterValidators)
        {
            _scooterService = scooterService;
            _scooterValidators = scooterValidators;
        }

        [Route("scooters")]
        [HttpPost]
        public IActionResult PostScooter(Scooter scooter)
        {
            if (!_scooterValidators.All(f => f.IsValid(scooter)))
            {
                return BadRequest();
            }

            if (_scooterService.GetAll().Any(s => s.Name == scooter.Name))
            {
                return BadRequest("Name should be unique");
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