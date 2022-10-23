using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRental.Exceptions;

namespace ScooterRental.tests
{
    [TestClass]
    public class RentalCompanyTests
    {
        private ScooterService _scooterService;
        private List<Scooter> _inventory;
        private RentalCompany _company;
        private List<RentedScooters> _rentedScooters;

        [TestInitialize]
        public void Setup()
        {
            _inventory = new List<Scooter>();
            _scooterService = new ScooterService(_inventory);
            _rentedScooters = new List<RentedScooters>();
            _company = new RentalCompany("Acme", _scooterService, _rentedScooters);
        }

        [TestMethod]
        public void StartRent_ChangeIsRentedStatus_IsRentedIsTrue()
        {
            //Arrange
            _scooterService.AddScooter("1", 0.2m);

            //Act
            _company.StartRent("1");

            //Assert
            _scooterService.GetScooterById("1").IsRented.Should().BeTrue();
        }

        [TestMethod]
        public void StartRent_AddScooterToRentedScootersList_ScooterAdded()
        {
            //Arrange
            _scooterService.AddScooter("1", 0.2m);

            //Act
            _company.StartRent("1");

            //Assert
            _rentedScooters.Count.Should().Be(1);
        }

        [TestMethod]
        public void StartRent_AddDuplicateScooterToRentedScootersList_ThrowsScooterAlreadyRentedException()
        {
            //Arrange
            _scooterService.AddScooter("1", 0.2m);

            //Act
            _company.StartRent("1");
            Action act = () => _company.StartRent("1");

            //Assert
            act.Should().Throw<ScooterAlreadyRentedException>()
                .WithMessage("Scooter with ID 1 already rented");
        }

        [TestMethod]
        public void StartRent_StartRentWithNonExistingScooter_ThrowsScooterDoesNotExistException()
        {
            //Act
            Action act = () => _company.StartRent("1");

            //Assert
            act.Should().Throw<ScooterDoesNotExistException>()
                .WithMessage("Scooter 1 does not exist");
        }

        [TestMethod]
        public void StartRent_StartRent_StartTimeAdded()
        {
            //Arrange
            _scooterService.AddScooter("1", 0.2m);

            //Act
            _company.StartRent("1");

            //Assert
            _rentedScooters[0].StartTime.Should().NotBe(null);
        }

        [TestMethod]
        public void EndRent_EndRentReturnsCost_CostReturned()
        {
            //Arrange
            _scooterService.AddScooter("1", 0.2m);

            //Act
            _company.StartRent("1");
            var cost = _company.EndRent("1");

            //Assert
            cost.Should().Be(0.2m);
        }

        [TestMethod]
        public void CalculateIncome_EndScooterRentAndCalculateIncome_ReturnsIncome()
        {
            //Arrange
            _scooterService.AddScooter("1", 0.2m);
            _company.StartRent("1");
            _company.EndRent("1");

            //Act
            var report = _company.CalculateIncome(DateTime.Now.Year, false);

            //Assert
            report.Should().Be(0.2m);
        }

        [TestMethod]
        public void CalculateIncome_SelectNullYear_ReturnsAllYearsIncome()
        {
            //Arrange
            _scooterService.AddScooter("1", 0.2m);
            _company.StartRent("1");
            _company.EndRent("1");

            //Act
            var income = _company.CalculateIncome(null, false);

            //Assert
            income.Should().Be(0.2m);
        }

        [TestMethod]
        public void CalculateIncome_SelectNoIncomeYear_ReturnsZero()
        {
            //Arrange
            _scooterService.AddScooter("1", 0.2m);
            _company.StartRent("1");
            _company.EndRent("1");

            //Act
            var income = _company.CalculateIncome(2001, false);

            //Assert
            income.Should().Be(0);
        }

        [TestMethod]
        public void CalculateIncome_IncludeNotCompletedRentals_ReturnsIncome()
        {
            //Arrange
            _scooterService.AddScooter("1", 0.2m);
            _company.StartRent("1");

            //Act
            var income = _company.CalculateIncome(DateTime.Now.Year, true);

            //Assert
            income.Should().Be(0.2m);
        }
    }
}
