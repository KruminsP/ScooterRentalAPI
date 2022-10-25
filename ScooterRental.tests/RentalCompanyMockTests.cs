//using System;
//using System.Collections.Generic;
//using FluentAssertions;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Moq.AutoMock;
//using ScooterRentalAPI.Core.Models;
//using ScooterRentalAPI.Exceptions;
//using ScooterRentalAPI.Interfaces;

//namespace ScooterRental.tests
//{
//    [TestClass]
//    public class RentalCompanyMockTests
//    {
//        private IRentalCompany _company;
//        private AutoMocker _mocker;
//        private Mock<IScooterService> _scooterServiceMock;
//        private List<RentedScooter> _rentedScooters;
//        private Scooter _defaultScooter;

//        [TestInitialize]
//        public void Setup()
//        {
//            _mocker = new AutoMocker();
//            _defaultScooter = new Scooter("1", 0.2m);
//            _scooterServiceMock = _mocker.GetMock<IScooterService>();
//            _rentedScooters = new List<RentedScooter>();
//            _company = new RentalCompany("Acme", _scooterServiceMock.Object, _rentedScooters);
//        }

//        [TestMethod]
//        public void StartRent_ChangeIsRentedStatus_IsRentedIsTrue()
//        {
//            //Arrange
//            _scooterServiceMock
//                .Setup(s => s.GetScooterById("1"))
//                .Returns(_defaultScooter);

//            //Act
//            _company.StartRent("1");

//            //Assert
//            _defaultScooter.IsRented.Should().BeTrue();
//        }

//        [TestMethod]
//        public void StartRent_AddScooterToRentedScootersList_ScooterAdded()
//        {
//            //Arrange
//            _scooterServiceMock
//                .Setup(s => s.GetScooterById("1"))
//                .Returns(_defaultScooter);

//            //Act
//            _company.StartRent("1");

//            //Assert
//            _rentedScooters.Count.Should().Be(1);
//        }

//        [TestMethod]
//        public void StartRent_AddDuplicateScooterToRentedScootersList_ThrowsScooterAlreadyRentedException()
//        {
//            //Arrange
//            _scooterServiceMock
//                .Setup(s => s.GetScooterById("1"))
//                .Returns(_defaultScooter);

//            //Act
//            _company.StartRent("1");
//            Action act = () => _company.StartRent("1");

//            //Assert
//            act.Should().Throw<ScooterAlreadyRentedException>()
//                .WithMessage("Scooter with ID 1 already rented");
//        }

//        [TestMethod]
//        public void StartRent_StartRentWithNonExistingScooter_ThrowsScooterDoesNotExistException()
//        {
//            //Arrange
//            _scooterServiceMock
//                .Setup(s => s.GetScooterById("1"))
//                .Throws(new ScooterDoesNotExistException("1"));

//            //Act
//            Action act = () => _company.StartRent("1");

//            //Assert
//            act.Should().Throw<ScooterDoesNotExistException>()
//                .WithMessage("Scooter 1 does not exist");
//        }

//        [TestMethod]
//        public void StartRent_StartRent_StartTimeAdded()
//        {
//            //Arrange
//            _scooterServiceMock
//                .Setup(s => s.GetScooterById("1"))
//                .Returns(_defaultScooter);

//            //Act
//            _company.StartRent("1");

//            //Assert
//            _rentedScooters[0].StartTime.Should().NotBe(null);
//        }

//        [TestMethod]
//        public void EndRent_EndRentReturnsCost_CostReturned()
//        {
//            //Arrange
//            _scooterServiceMock
//                .Setup(s => s.GetScooterById("1"))
//                .Returns(_defaultScooter);

//            //Act
//            _company.StartRent("1");
//            var cost = _company.EndRent("1");

//            //Assert
//            cost.Should().Be(0.2m);
//        }

//        [TestMethod]
//        public void CalculateIncome_EndScooterRentAndCalculateIncome_ReturnsIncome()
//        {
//            //Arrange
//            _scooterServiceMock
//                .Setup(s => s.GetScooterById("1"))
//                .Returns(_defaultScooter);
//            _company.StartRent("1");
//            _company.EndRent("1");

//            //Act
//            var report = _company.CalculateIncome(DateTime.Now.Year, false);

//            //Assert
//            report.Should().Be(0.2m);
//        }

//        [TestMethod]
//        public void CalculateIncome_SelectNullYear_ReturnsAllYearsIncome()
//        {
//            //Arrange
//            _scooterServiceMock
//                .Setup(s => s.GetScooterById("1"))
//                .Returns(_defaultScooter);
//            _company.StartRent("1");
//            _company.EndRent("1");

//            //Act
//            var income = _company.CalculateIncome(null, false);

//            //Assert
//            income.Should().Be(0.2m);
//        }

//        [TestMethod]
//        public void CalculateIncome_SelectNoIncomeYear_ReturnsZero()
//        {
//            //Arrange
//            _scooterServiceMock
//                .Setup(s => s.GetScooterById("1"))
//                .Returns(_defaultScooter);
//            _company.StartRent("1");
//            _company.EndRent("1");

//            //Act
//            var income = _company.CalculateIncome(2001, false);

//            //Assert
//            income.Should().Be(0);
//        }

//        [TestMethod]
//        public void CalculateIncome_IncludeNotCompletedRentals_ReturnsIncome()
//        {
//            //Arrange
//            _scooterServiceMock
//                .Setup(s => s.GetScooterById("1"))
//                .Returns(_defaultScooter);
//            _company.StartRent("1");

//            //Act
//            var income = _company.CalculateIncome(DateTime.Now.Year, true);

//            //Assert
//            income.Should().Be(0.2m);
//        }
//    }
//}
