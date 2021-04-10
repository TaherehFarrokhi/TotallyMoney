using System;
using CustomerMarketing.Core.Domain;
using CustomerMarketing.Core.Strategies;
using FluentAssertions;
using Xunit;

namespace CustomerMarketing.Core.Tests.Strategies
{
    public class MonthlyNotificationScheduleStrategyTests : IClassFixture<NotificationScheduleStrategyFixture>
    {
        private readonly NotificationScheduleStrategyFixture _fixture;

        public MonthlyNotificationScheduleStrategyTests(NotificationScheduleStrategyFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Should_ConfiguredCorrectly()
        {
            // Arrange

            // Act
            var sut = new MonthlyNotificationScheduleStrategy();

            // Assert
            sut.Mode.Should().Be(SubscriptionMode.Monthly);
        }

        [Fact]
        public void Should_CalculateSchedule_ReturnTheCorrectDateRange_WhenStartDateAndNumberOfDaysAreValid()
        {
            // Arrange
            var sut = new MonthlyNotificationScheduleStrategy();

            // Act
            var actual = sut.CalculateSchedule(new MonthlySubscription(new []{2, 5}), new DateTime(2021, 04, 2), 10);

            // Assert
            actual.Should().ContainInOrder(DateTime.Parse("2021-04-02"), DateTime.Parse("2021-04-05"));
        }

        [Theory]
        [InlineData(SubscriptionMode.Daily)]
        [InlineData(SubscriptionMode.Never)]
        [InlineData(SubscriptionMode.Weekly)]
        public void Should_CalculateSchedule_ThrowException_WhenTheSubscriptionTypeIsIncorrect(SubscriptionMode mode)
        {
            // Arrange
            var sut = new MonthlyNotificationScheduleStrategy();

            // Act
            Action action = () => sut.CalculateSchedule(_fixture.GetSubscription(mode), DateTime.Today, 1);

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}