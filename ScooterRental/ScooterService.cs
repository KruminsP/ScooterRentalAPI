using ScooterRental.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ScooterRental.Exceptions;
using ScooterRental.Validators;

namespace ScooterRental
{
    public class ScooterService : IScooterService
    {
        private readonly List<Scooter> _scooters;

        public ScooterService(List<Scooter> inventory)
        { 
            _scooters = inventory;
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            if (_scooters.Any(scooter => scooter.Id == id))
            {
                throw new DuplicateScooterException(id);
            }

            if (pricePerMinute <= 0)
            {
                throw new InvalidPriceException(pricePerMinute);
            }

            Validator.ScooterIdValidator(id);

            _scooters.Add(new Scooter(id, pricePerMinute));
        }

        public void RemoveScooter(string id)
        {
            Validator.ScooterIdValidator(id);

            Scooter scooter = _scooters.FirstOrDefault(scooter => scooter.Id == id);

            Validator.ScooterExistingValidator(scooter, id);

            _scooters.Remove(scooter);
        }

        public IList<Scooter> GetScooters()
        {
            return _scooters.ToList();
        }

        public Scooter GetScooterById(string scooterId)
        {
            Scooter scooter = _scooters.FirstOrDefault(scooter => scooter.Id == scooterId);

            Validator.ScooterExistingValidator(scooter, scooterId);

            return _scooters.FirstOrDefault(scooterById => scooterById.Id == scooterId);
        }
    }
}
