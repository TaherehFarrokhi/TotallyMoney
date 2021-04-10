using System;
using System.Collections.Generic;
using System.Linq;
using CustomerMarketing.Core.Domain;
using CustomerMarketing.Core.Extensions;

namespace CustomerMarketing.Core.Strategies
{
    public sealed class MonthlyNotificationScheduleStrategy : INotificationScheduleStrategy
    {
        public SubscriptionMode Mode => SubscriptionMode.Monthly;

        public IEnumerable<DateTime> CalculateSchedule(ISubscription subscription, DateTime startDate, int numberOfDays)
        {
            if (subscription == null)
                throw new ArgumentNullException(nameof(subscription));

            if (subscription is not MonthlySubscription monthlySubscription)
                throw new ArgumentException("Subscription type is not monthly", nameof(subscription));

            if (numberOfDays <= 0)
                throw new ArgumentOutOfRangeException(nameof(numberOfDays));

            return startDate.ToDateRange(numberOfDays)
                .Where(d => monthlySubscription.Days.Contains(d.Day))
                .ToArray();
        }
    }
}