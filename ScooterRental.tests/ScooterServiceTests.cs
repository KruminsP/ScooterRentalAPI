//using System;
//using System.Collections.Generic;
//using System.Linq;
//using FluentAssertions;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ScooterRental.Exceptions;
//using ScooterRental.Interfaces;

//namespace ScooterRental.tests
//{
//    [TestClass]
//    public class ScooterServiceTests
//    {
//        private IScooterService _scooterService;
//        private List<Scooter> _inventory;

//        [TestInitialize]
//        public void Setup()
//        {
//            _inventory = new List<Scooter>();
//            _scooterService = new ScooterService(_inventory);
//        }

//        [TestMethod]
//        public void AddScooter_AddValidScooter_ScooterAdded()
//        {
//            //Act
//            _scooterService.AddScooter("1", 0.2m);

//            //Assert
//            _inventory.Count.Should().Be(1);
//        }

//        [TestMethod]
//        public void AddScooter_AddScooterTwice_ThrowsDuplicateScooterException()
//        {
//            //Arrange
//            _scooterService.AddScooter("1", 0.2m);

//            //Act
//            Action act = () => _scooterService.AddScooter("1", 0.2m);

//            //Assert
//            act.Should().Throw<DuplicateScooterException>().WithMessage("Scooter with id 1 already exists");
//        }

//        [TestMethod]
//        public void AddScooter_AddScooterWithPriceZeroOrLess_ThrowsInvalidPriceException()
//        {
//            //Act
//            Action act = () => _scooterService.AddScooter("1", -0.2m);

//            //Assert
//            act.Should().Throw<InvalidPriceException>().WithMessage("Given price -0.2 not valid");
//        }

//        [TestMethod]
//        public void AddScooter_AddScooterWithNullOrEmptyId_ThrowsInvalidIdException()
//        {
//            //Act
//            Action act = () => _scooterService.AddScooter("", 0.2m);

//            //Assert
//            act.Should().Throw<InvalidIdException>().WithMessage("ID cannot be null or empty");
//        }

//        [TestMethod]
//        public void RemoveScooter_ScooterExists_ScooterIsRemoved()
//        {
//            //Arrange
//            _inventory.Add(new Scooter("1", 0.2m));

//            //Act
//            _scooterService.RemoveScooter("1");

//            //Assert
//            _inventory.Any(scooter => scooter.Id == "1").Should().BeFalse();
//            _inventory.Count.Should().Be(0);
//        }

//        [TestMethod]
//        public void RemoveScooter_ScooterDoesntExist_ThrowsScooterDoesntExistException()
//        {
//            //Act
//            Action act = () => _scooterService.RemoveScooter("8");

//            //Assert
//            act.Should().Throw<ScooterDoesNotExistException>().WithMessage("Scooter 8 does not exist");
//        }

//        [TestMethod]
//        public void RemoveScooter_RemoveScooterWithNullOrEmptyId_ThrowsInvalidIdException()
//        {
//            //Act
//            Action act = () => _scooterService.RemoveScooter("");

//            //Assert
//            act.Should().Throw<InvalidIdException>()
//                .WithMessage("ID cannot be null or empty");
//        }

//        [TestMethod]
//        public void GetScooters_ReturnScooterList_ScooterListReturned()
//        {
//            //Arrange
//            var testInventory = new List<Scooter>();
//            testInventory.Add(new Scooter("1", 0.2m));

//            //Act
//            _scooterService.AddScooter("1", 0.2m);

//            //Assert
//            testInventory.Should().BeEquivalentTo(_scooterService.GetScooters());
//        }

//        [TestMethod]
//        public void GetScooterById_ReturnScooter_ScooterReturned()
//        {
//            //Arrange
//            _scooterService.AddScooter("1", 0.2m);

//            //Act
//            var returnedScooter = _scooterService.GetScooterById("1");

//            //Assert
//            returnedScooter.Should().BeEquivalentTo(new Scooter("1", 0.2m));
//        }

//        [TestMethod]
//        public void GetScooterById_GetScooterWithNullOrEmptyId_ThrowsInvalidIdException()
//        {
//            //Act
//            Action act = () => _scooterService.GetScooterById("");

//            //Assert
//            act.Should().Throw<ScooterDoesNotExistException>();
//        }

//        [TestMethod]
//        public void GetScooterById_ScooterDoesNotExist_ThrowsScooterDoesNotExistException()
//        {
//            //Act
//            Action act = () => _scooterService.GetScooterById("1");

//            //Assert
//            act.Should().Throw<ScooterDoesNotExistException>();
//        }
//    }
//}