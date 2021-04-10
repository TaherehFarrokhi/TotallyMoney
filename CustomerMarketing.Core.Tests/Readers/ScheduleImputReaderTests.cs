using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CustomerMarketing.Core.Domain;
using CustomerMarketing.Core.Readers;
using CustomerMarketing.Core.Strategies;
using CustomerMarketing.Core.Writers;
using FluentAssertions;
using Moq;
using Xunit;

namespace CustomerMarketing.Core.Tests.Readers
{
    public class ScheduleInputReaderTests
    {
        [Fact]
        public void Should_Read_SuccessfullyGenerateCustomers_WhenTheFileFormatIsValid()
        {
            // Arrange
            var content = new[]
            {
                "Alice|Daily",
                "John|Monthly|10,12",
                "Edward|Never",
                "Vicky|Weekly|Sunday,Monday"
            };
            var reader = new Mock<IFileReader>();
            reader.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            reader.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(content);
            
            var sut = new ScheduleInputReader(reader.Object, new InputSettings());

            // Act
            var actual = sut.Read("schedule.csv");

            // Assert
            actual.Should().HaveCount(4)
                .And.Contain(m => m.Name == "Alice" && m.Subscription is DailySubscription)
                .And.Contain(m => m.Name == "John" &&
                                  m.Subscription is MonthlySubscription &&
                                  ((MonthlySubscription) m.Subscription).Days.Contains(10) &&
                                  ((MonthlySubscription) m.Subscription).Days.Contains(12))
                .And.Contain(m => m.Name == "Edward" && m.Subscription is NeverSubscription)
                .And.Contain(m => m.Name == "Vicky" &&
                                  m.Subscription is WeeklySubscription &&
                                  ((WeeklySubscription) m.Subscription).Days.Contains(DayOfWeek.Sunday) &&
                                  ((WeeklySubscription) m.Subscription).Days.Contains(DayOfWeek.Monday));
        }          
        
        [Fact]
        public void Should_Read_ThrowFileNotFoundException_WhenTheFileIsNotAvailable()
        {
            // Arrange
            var reader = new Mock<IFileReader>();
            reader.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
            
            var sut = new ScheduleInputReader(reader.Object, new InputSettings());

            // Act
            Action action = () => sut.Read("schedule.csv");

            // Assert
            action.Should().Throw<FileNotFoundException>();
        }   
        
        [Fact]
        public void Should_Read_ThrowInvalidRowException_WhenThereAreRowsWithLessThenTwoColumns()
        {
            // Arrange
            var content = new[]
            {
                "Alice",
                "John|Monthly|10,12",
            };
            var reader = new Mock<IFileReader>();
            reader.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            reader.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(content);
            
            var sut = new ScheduleInputReader(reader.Object, new InputSettings());

            // Act
            Action action = () => sut.Read("schedule.csv");

            // Assert
            action.Should().Throw<InvalidRowException>().Where(m => m.Line == "Alice");
        } 
        
        [Theory]
        [InlineData("John|Monthly", SubscriptionMode.Monthly)]
        [InlineData("John|Weekly", SubscriptionMode.Weekly)]
        public void Should_Read_ThrowInvalidRowException_WhenThereAreMonthlyOrWeeklyRowsWithoutExtraInfoColumn(string invalidRow, SubscriptionMode mode)
        {
            // Arrange
            var content = new[]
            {
                "Alice|Never",
                invalidRow
            };
            var reader = new Mock<IFileReader>();
            reader.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            reader.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(content);
            
            var sut = new ScheduleInputReader(reader.Object, new InputSettings());

            // Act
            Action action = () => sut.Read("schedule.csv");

            // Assert
            action.Should().Throw<InvalidRowException>()
                .WithMessage($"Invalid row format for {mode.ToString()}. missing extra info")
                .Where(m => m.Line == invalidRow);
        } 
        
                
        [Theory]
        [InlineData("John|Monthly|1,110", "Days is not valid, they should be between 1 and 28")]
        [InlineData("John|Monthly|1,29", "Days is not valid, they should be between 1 and 28")]
        [InlineData("John|Monthly|0,18", "Days is not valid, they should be between 1 and 28")]
        public void Should_Read_ThrowInvalidRowException_WhenMonthlyRowHasOutOfRangeDay(string invalidRow, string message)
        {
            // Arrange
            var content = new[]
            {
                "Alice|Never",
                invalidRow
            };
            var reader = new Mock<IFileReader>();
            reader.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            reader.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(content);
            
            var sut = new ScheduleInputReader(reader.Object, new InputSettings());

            // Act
            Action action = () => sut.Read("schedule.csv");

            // Assert
            action.Should().Throw<InvalidRowException>()
                .WithMessage(message)
                .Where(m => m.Line == invalidRow);
        } 
    }
}