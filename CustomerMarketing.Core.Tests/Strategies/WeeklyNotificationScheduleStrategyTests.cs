using System;
using CustomerMarketing.Core.Domain;
using CustomerMarketing.Core.Strategies;
using FluentAssertions;
using Xunit;

namespace CustomerMarketing.Core.Tests.Strategies
{
    public class WeeklyNotificationScheduleStrategyTests : IClassFixture<NotificationScheduleStrategyFixture>
    {
        private readonly NotificationScheduleStrategyFixture _fixture;

        public WeeklyNotificationScheduleStrategyTests(NotificationScheduleStrategyFixture fixture)
        {
            _fixture = fixture;
        }
        
        [Fact]
        public void Should_ConfiguredCorrectly()
        {
            // Arrange

            // Act
            var sut = new WeeklyNotificationScheduleStrategy();

            // Assert
            sut.Mode.Should().Be(SubscriptionMode.Weekly);
        }         
        
        [Fact]
        public void Should_CalculateSchedule_ReturnTheCorrectDateRange_WhenStartDateAndNumberOfDaysAreValid()
        {
            // Arrange
            var sut = new WeeklyNotificationScheduleStrategy();

            // Act
            var actual = sut.CalculateSchedule(new WeeklySubscription(new [] {DayOfWeek.Saturday, DayOfWeek.Monday}), new DateTime(2021,04,10), 7);

            // Assert
            actual.Should().ContainInOrder(DateTime.Parse("2021-04-10"), DateTime.Parse("2021-04-12"));
        }  
        
        [Theory]
        [InlineData(SubscriptionMode.Monthly)]
        [InlineData(SubscriptionMode.Never)]
        [InlineData(SubscriptionMode.Daily)]
        public void Should_CalculateSchedule_ThrowException_WhenTheSubscriptionTypeIsIncorrect(SubscriptionMode mode)
        {
            // Arrange
            var sut = new WeeklyNotificationScheduleStrategy();

            // Act
            Action action = () => sut.CalculateSchedule(_fixture.GetSubscription(mode), DateTime.Today, 1);

            // Assert
            action.Should().Throw<ArgumentException>();
        }  
    }
}