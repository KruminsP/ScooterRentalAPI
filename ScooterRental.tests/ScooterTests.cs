//using FluentAssertions;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ScooterRentalAPI.Core.Models;

//namespace ScooterRental.tests
//{
//    [TestClass]
//    public class ScooterTests
//    {
//        private Scooter _scooter;

//        [TestMethod]
//        public void ScooterCreation_IDAndPricePerMinuteSetCorrectly()
//        {
//            //Act
//            _scooter = new Scooter("1", 0.2m);

//            //Assert
//            _scooter.Id.Should().Be("1");
//            _scooter.PricePerMinute.Should().Be(0.2m);
//            _scooter.IsRented.Should().BeFalse();
//        }
//    }
//}
