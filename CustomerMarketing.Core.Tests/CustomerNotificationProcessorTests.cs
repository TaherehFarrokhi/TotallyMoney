using System;
using System.Collections.Generic;
using CustomerMarketing.Core.Domain;
using CustomerMarketing.Core.Readers;
using CustomerMarketing.Core.Strategies;
using CustomerMarketing.Core.Writers;
using FluentAssertions;
using Moq;
using Xunit;

namespace CustomerMarketing.Core.Tests
{
    public class CustomerNotificationProcessorTests
    {
        [Fact]
        public void Should_Process_ProcessTheInputCorrectlyAndGenerateTheOutput()
        {
            // Arrange
            var reader = new Mock<IScheduleInputReader>();
            var customer = new Customer("Name", new DailySubscription());
            reader.Setup(r => r.Read(It.IsAny<string>()))
                .Returns(new List<Customer> {customer});
            
            var writer = new Mock<INotificationsOutputWriter>();
            var strategy = new Mock<INotificationScheduleStrategy>();
            strategy.Setup(s => s.Mode).Returns(SubscriptionMode.Daily);
            
            var sut = new CustomerNotificationProcessor(reader.Object, writer.Object, new []{strategy.Object});

            // Act
            sut.Process("schedule.csv", DateTime.Today, 2);

            // Assert
            reader.Verify(x => x.Read(It.IsAny<string>()), Times.Once);
            strategy.Verify(x => x.CalculateSchedule(customer.Subscription, DateTime.Today, 2), Times.Once);
            writer.Verify(x => x.Write(It.IsAny<IEnumerable<CustomerNotificationSchedule>>(), DateTime.Today, 2), Times.Once);
        }        
        
        [Fact]
        public void Should_Process_ThrowErrorWhenCannotFindTheRelevantStrategy()
        {
            // Arrange
            var reader = new Mock<IScheduleInputReader>();
            var customer = new Customer("Name", new DailySubscription());
            reader.Setup(r => r.Read(It.IsAny<string>()))
                .Returns(new List<Customer> {customer});
            
            var writer = new Mock<INotificationsOutputWriter>();
            var strategy = new Mock<INotificationScheduleStrategy>();
            strategy.Setup(s => s.Mode).Returns(SubscriptionMode.Monthly);
            
            var sut = new CustomerNotificationProcessor(reader.Object, writer.Object, new []{strategy.Object});

            // Act
            Action action = () => sut.Process("schedule.csv", DateTime.Today, 2);

            // Assert
            action.Should().Throw<ScheduleStrategyNotFoundException>();
        }
    }
}