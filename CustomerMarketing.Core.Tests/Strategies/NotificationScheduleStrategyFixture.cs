using System;
using System.Collections.Generic;
using CustomerMarketing.Core.Domain;

namespace CustomerMarketing.Core.Tests.Strategies
{
    public class NotificationScheduleStrategyFixture
    {
        private readonly Dictionary<SubscriptionMode, Func<ISubscription>> _subscriptions =
            new()
            {
                {SubscriptionMode.Never, () => new NeverSubscription()},
                {SubscriptionMode.Daily, () => new DailySubscription()},
                {SubscriptionMode.Weekly, () => new WeeklySubscription(new [] {DayOfWeek.Friday, DayOfWeek.Monday})},
                {SubscriptionMode.Monthly, () => new MonthlySubscription(new []{2, 5, 28})},
            };

        public ISubscription GetSubscription(SubscriptionMode mode) => _subscriptions[mode]();
    }
}