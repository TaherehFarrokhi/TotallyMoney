using System;
using System.Collections.Generic;
using CustomerMarketing.Core.Domain;
using CustomerMarketing.Core.Extensions;

namespace CustomerMarketing.Core.Strategies
{
    public sealed class DailyNotificationScheduleStrategy : INotificationScheduleStrategy
    {
        public SubscriptionMode Mode => SubscriptionMode.Daily;

        public IEnumerable<DateTime> CalculateSchedule(ISubscription subscription, DateTime startDate, int numberOfDays)
        {
            if (subscription == null)
                throw new ArgumentNullException(nameof(subscription));

            if (subscription is not DailySubscription)
                throw new ArgumentException(null, nameof(subscription));

            if (numberOfDays <= 0)
                throw new ArgumentOutOfRangeException(nameof(numberOfDays));

            return startDate.ToDateRange(numberOfDays);
        }
    }
}