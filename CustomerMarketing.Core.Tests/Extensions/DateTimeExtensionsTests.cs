using System;
using CustomerMarketing.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace CustomerMarketing.Core.Tests.Extensions
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void Should_ToDateRange_ReturnTheRightRangeOfDates_WhenNumberOfDateIsValid()
        {
            // Arrange

            // Act
            var actual = DateTime.Today.ToDateRange(2);

            // Assert
            actual.Should().ContainInOrder(DateTime.Today, DateTime.Today.AddDays(1));
        }        
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_ToDateRange_ThrowArgumentOutOfRangeException_WhenNumberOfDaysIsNotValid(int numberOfDays)
        {
            // Arrange

            // Act
            Action action = () => DateTime.Today.ToDateRange(numberOfDays);

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}