using System;
using System.Collections.Generic;
using CustomerMarketing.Core.Domain;
using CustomerMarketing.Core.Strategies;
using FluentAssertions;
using Xunit;

namespace CustomerMarketing.Core.Tests.Strategies
{
    public class DailyNotificationScheduleStrategyTests : IClassFixture<NotificationScheduleStrategyFixture>
    {
        private readonly NotificationScheduleStrategyFixture _fixture;

        public DailyNotificationScheduleStrategyTests(NotificationScheduleStrategyFixture fixture)
        {
            _fixture = fixture;
        }
        
        [Fact]
        public void Should_ConfiguredCorrectly()
        {
            // Arrange

            // Act
            var sut = new DailyNotificationScheduleStrategy();

            // Assert
            sut.Mode.Should().Be(SubscriptionMode.Daily);
        }         
        
        [Fact]
        public void Should_CalculateSchedule_ReturnTheCorrectDateRange_WhenStartDateAndNumberOfDaysAreValid()
        {
            // Arrange
            var sut = new DailyNotificationScheduleStrategy();

            // Act
            var actual = sut.CalculateSchedule(new DailySubscription(), DateTime.Today, 1);

            // Assert
            actual.Should().ContainInOrder(DateTime.Today);
        }  
        
        [Theory]
        [InlineData(SubscriptionMode.Monthly)]
        [InlineData(SubscriptionMode.Never)]
        [InlineData(SubscriptionMode.Weekly)]
        public void Should_CalculateSchedule_ThrowException_WhenTheSubscriptionTypeIsIncorrect(SubscriptionMode mode)
        {
            // Arrange
            var sut = new DailyNotificationScheduleStrategy();

            // Act
            Action action = () => sut.CalculateSchedule(_fixture.GetSubscription(mode), DateTime.Today, 1);

            // Assert
            action.Should().Throw<ArgumentException>();
        }  
    }
}