using System;
using System.Collections.Generic;
using CustomerMarketing.Core.Domain;

namespace CustomerMarketing.Core.Strategies
{
    public sealed class NeverNotificationScheduleStrategy : INotificationScheduleStrategy
    {
        public SubscriptionMode Mode => SubscriptionMode.Never;

        public IEnumerable<DateTime> CalculateSchedule(ISubscription subscription, DateTime startDate, int numberOfDays)
        {
            if (subscription == null)
                throw new ArgumentNullException(nameof(subscription));

            if (subscription is not NeverSubscription monthlySubscription)
                throw new ArgumentException("Subscription type is not monthly", nameof(subscription));
            
            return new List<DateTime>();
        }
    }
}