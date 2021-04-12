using System;
using System.IO;
using CustomerMarketing.Core.Domain;
using CustomerMarketing.Core.Writers;
using FluentAssertions;
using Xunit;

namespace CustomerMarketing.Core.Tests.Writers
{
    public class DailyNotificationsOutputWriterTests
    {
        [Fact]
        public void Should_Write_GenerateOrderedListOfSchedulesForAllGivenCustomers()
        {
            // Arrange
            static string GenerateOutput(DateTime date, string name) =>
                $"{date.ToString("ddd dd-MMMM-yyyy").PadRight(21)}\t{name}";
            
            var output = Console.Out;
            var writer = new StringWriter();
            Console.SetOut(writer);
            
            var customers = new[]
            {
                new CustomerNotificationSchedule("C#1", new[] {DateTime.Today, DateTime.Today.AddDays(2)}),
                new CustomerNotificationSchedule("C#2", new[] {DateTime.Today, DateTime.Today.AddDays(3)}),
            };
            
            var sut = new DailyNotificationsOutputWriter();

            // Act
            sut.Write(customers, DateTime.Today, 4);

            // Assert
            var actual = writer.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            actual.Should().ContainInOrder(
                GenerateOutput(DateTime.Today, "C#1, C#2"),
                GenerateOutput(DateTime.Today.AddDays(1), string.Empty),
                GenerateOutput(DateTime.Today.AddDays(2), "C#1"),
                GenerateOutput(DateTime.Today.AddDays(3), "C#2"));
            
            Console.SetOut(output);
        }
    }
}