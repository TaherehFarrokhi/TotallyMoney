using System;
using System.Linq;
using CustomerMarketing.Core.Readers;

namespace CustomerMarketing.Core.Domain
{
    public sealed class MonthlySubscription : ISubscription
    {
        public MonthlySubscription(int[] days)
        {
            Days = days;
        }

        public int[] Days { get; init; }
        public SubscriptionMode Mode => SubscriptionMode.Monthly;

        public static MonthlySubscription FromValues(string line, string[] values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            var array= values.Select(int.Parse).ToArray();

            if (array.Any(x => x is > 28 or < 1))
                throw new InvalidRowException("Days is not valid, they should be between 1 and 28", line);
            
            return new MonthlySubscription(array);
        }
    }
}