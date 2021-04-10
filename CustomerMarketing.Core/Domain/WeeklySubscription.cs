using System;
using System.Linq;
using EnumsNET;

namespace CustomerMarketing.Core.Domain
{
    public sealed class WeeklySubscription : ISubscription
    {
        public WeeklySubscription(DayOfWeek[] days)
        {
            Days = days;
        }

        public DayOfWeek[] Days { get; init; }
        public SubscriptionMode Mode => SubscriptionMode.Weekly;
        
        public static WeeklySubscription FromValues(string[] values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            return new WeeklySubscription(values.Select(Enums.Parse<DayOfWeek>).ToArray());
        }
    }
}