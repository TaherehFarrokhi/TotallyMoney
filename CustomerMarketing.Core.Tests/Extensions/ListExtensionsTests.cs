using System.Collections.Generic;
using CustomerMarketing.Core.Extensions;
using CustomerMarketing.Core.Writers;
using FluentAssertions;
using Xunit;

namespace CustomerMarketing.Core.Tests.Extensions
{
    public class ListExtensionsTests
    {
        [Fact]
        public void
            Should_FirstItemPropertyOrDefault_ReturnThePropertyValueForFirstObjectInTheList_WhenTheListIsNotEmpty()
        {
            // Arrange
            var list = new List<KeyValuePair<string, string>> {new("Key1", "Value1"), new("Key2", "Value2")};

            // Act
            var actual = list.FirstItemPropertyOrDefault(m => m.Value);

            // Assert
            actual.Should().Be("Value1");
        }

        [Fact]
        public void Should_FirstItemPropertyOrDefault_ReturnTheDefaultPropertyValue_WhenTheListIsEmpty()
        {
            // Arrange

            // Act
            var actual = new List<KeyValuePair<string, string>>().FirstItemPropertyOrDefault(m => m.Value);

            // Assert
            actual.Should().BeNull();
        }
    }
}