using System;
using CustomerMarketing.Core.Domain;

namespace CustomerMarketing.Core
{
    public class ScheduleStrategyNotFoundException : Exception
    {
        public ScheduleStrategyNotFoundException(SubscriptionMode subscriptionMode) : base(
            $"Strategy for handling {subscriptionMode} is not configured correctly")
        {
        }
    }
}