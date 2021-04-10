using System;
using CustomerMarketing.Core.Domain;
using CustomerMarketing.Core.Strategies;
using FluentAssertions;
using Xunit;

namespace CustomerMarketing.Core.Tests.Strategies
{
    public class NeverNotificationScheduleStrategyTests : IClassFixture<NotificationScheduleStrategyFixture>
    {
        private readonly NotificationScheduleStrategyFixture _fixture;

        public NeverNotificationScheduleStrategyTests(NotificationScheduleStrategyFixture fixture)
        {
            _fixture = fixture;
        }
        
        [Fact]
        public void Should_ConfiguredCorrectly()
        {
            // Arrange

            // Act
            var sut = new NeverNotificationScheduleStrategy();

            // Assert
            sut.Mode.Should().Be(SubscriptionMode.Never);
        }         
        
        [Fact]
        public void Should_CalculateSchedule_ReturnTheCorrectDateRange_WhenStartDateAndNumberOfDaysAreValid()
        {
            // Arrange
            var sut = new NeverNotificationScheduleStrategy();

            // Act
            var actual = sut.CalculateSchedule(new NeverSubscription(), DateTime.Today, 1);

            // Assert
            actual.Should().BeEmpty();
        }  
        
        [Theory]
        [InlineData(SubscriptionMode.Monthly)]
        [InlineData(SubscriptionMode.Daily)]
        [InlineData(SubscriptionMode.Weekly)]
        public void Should_CalculateSchedule_ThrowException_WhenTheSubscriptionTypeIsIncorrect(SubscriptionMode mode)
        {
            // Arrange
            var sut = new NeverNotificationScheduleStrategy();

            // Act
            Action action = () => sut.CalculateSchedule(_fixture.GetSubscription(mode), DateTime.Today, 1);

            // Assert
            action.Should().Throw<ArgumentException>();
        }  
    }
}