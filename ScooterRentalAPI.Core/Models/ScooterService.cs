using ScooterRentalAPI.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ScooterRentalAPI.Core.Validators;
using ScooterRentalAPI.Exceptions;

namespace ScooterRentalAPI.Core.Models
{
    public class ScooterService : IScooterService
    {
        private static readonly List<Scooter> _scooters = new List<Scooter>();
        private static int _Id = 0;

        public static Scooter AddScooter(Scooter scooter)
        {

            if (_scooters.Any(s => s.Id == scooter.Id))
            {
                throw new DuplicateScooterException(scooter.Id);
            }

            scooter.Id = ++_Id;
            _scooters.Add(scooter);

            return scooter;


            //if (pricePerMinute <= 0)
            //{
            //    throw new InvalidPriceException(pricePerMinute);
            //}

            //Validator.ScooterIdValidator(id);
            //var scooter = new Scooter(id, pricePerMinute);
            //_scooters.Add(scooter);

            //return scooter;
        }

        public static void RemoveScooter(int id)
        {
            Validator.ScooterIdValidator(id);

            Scooter scooter = _scooters.FirstOrDefault(scooter => scooter.Id == id);

           // Validator.ScooterExistingValidator(scooter, id);

            _scooters.Remove(scooter);
        }

        public static IList<Scooter> GetScooters()
        {
            return _scooters.ToList();
        }

        public static Scooter GetScooterById(int scooterId)
        {
            Scooter scooter = _scooters.FirstOrDefault(scooter => scooter.Id == scooterId);

            //Validator.ScooterExistingValidator(scooter, scooterId);

            return _scooters.FirstOrDefault(scooterById => scooterById.Id == scooterId);
        }

        public static bool ScooterExists(Scooter scooter)
        {
            if (_scooters.Any(s => s.Id == scooter.Id))
            {
                return true;
            }

            return false;
        }
    }
}
