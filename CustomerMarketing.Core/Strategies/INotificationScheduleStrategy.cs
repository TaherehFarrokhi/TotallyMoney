using System;
using System.Collections.Generic;
using CustomerMarketing.Core.Domain;

namespace CustomerMarketing.Core.Strategies
{
    public interface INotificationScheduleStrategy
    {
        SubscriptionMode Mode { get; }
        IEnumerable<DateTime> CalculateSchedule(ISubscription subscription, DateTime startDate, int numberOfDays);
    }
}