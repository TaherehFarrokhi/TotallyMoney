namespace CustomerMarketing.Core.Domain
{
    public sealed class DailySubscription : ISubscription
    {
        public SubscriptionMode Mode => SubscriptionMode.Daily;
    }
}