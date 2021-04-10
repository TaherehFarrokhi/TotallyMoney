using System;
using System.Collections.Generic;
using System.Linq;
using CustomerMarketing.Core.Domain;
using CustomerMarketing.Core.Extensions;

namespace CustomerMarketing.Core.Strategies
{
    public sealed class WeeklyNotificationScheduleStrategy : INotificationScheduleStrategy
    {
        public SubscriptionMode Mode => SubscriptionMode.Weekly;

        public IEnumerable<DateTime> CalculateSchedule(ISubscription subscription, DateTime startDate, int numberOfDays)
        {
            if (subscription == null)
                throw new ArgumentNullException(nameof(subscription));

            if (subscription is not WeeklySubscription weeklySubscription)
                throw new ArgumentException(null, nameof(subscription));

            if (numberOfDays <= 0)
                throw new ArgumentOutOfRangeException(nameof(numberOfDays));

            return startDate.ToDateRange(numberOfDays)
                .Where(d => weeklySubscription.Days.Contains(d.DayOfWeek))
                .ToArray();
        }
    }
}