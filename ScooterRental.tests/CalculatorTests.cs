using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRentalAPI.Core.Models;
using System;

namespace ScooterRental.tests
{
    [TestClass]
    public class CalculatorTests
    {
        private Calculator _feeCalculator;

        [TestInitialize]
        public void Setup()
        {
            _feeCalculator = new Calculator();
        }

        [TestMethod]
        public void ScooterFeeCalculator_SameDayReturn_ReturnsIncome()
        {
            //Arrange
            var start = new DateTime(2000, 1, 1, 0, 0, 0);
            var end = new DateTime(2000, 1, 1, 1, 0, 0);
            var pricePerMinute = 0.2m;

            //Act
            var fee = _feeCalculator.ScooterFeeCalculator(start, end, pricePerMinute);

            //Assert
            fee.Should().Be(12);
        }

        [TestMethod]
        public void ScooterFeeCalculator_DifferentDaysMonthsAndYears_ReturnsIncome()
        {
            //Arrange
            var start = new DateTime(1999, 12, 31, 23, 59, 0);
            var end = new DateTime(2000, 1, 1, 0, 1, 0);
            var pricePerMinute = 1;

            //Act
            var fee = _feeCalculator.ScooterFeeCalculator(start, end, pricePerMinute);

            //Assert
            fee.Should().Be(2);
        }

        [TestMethod]
        public void ScooterFeeCalculator_FeeExceeds20PerDay_ReturnsIncome()
        {
            //Arrange
            var start = new DateTime(1999, 12, 31, 0, 0, 0);
            var end = new DateTime(2000, 1, 1, 0, 1, 0);
            var pricePerMinute = 1;

            //Act
            var fee = _feeCalculator.ScooterFeeCalculator(start, end, pricePerMinute);

            //Assert
            fee.Should().Be(21);
        }
    }
}
