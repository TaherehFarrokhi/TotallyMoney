using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerMarketing.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static IEnumerable<DateTime> ToDateRange(this DateTime dateTime, int numberOfDays)
        {
            if (numberOfDays <= 0)
                throw new ArgumentOutOfRangeException(nameof(numberOfDays));

            var dayRange = Enumerable.Range(0, numberOfDays);
            return dayRange.Select(d => dateTime.Date.AddDays(d)).ToArray();
        }
    }
}