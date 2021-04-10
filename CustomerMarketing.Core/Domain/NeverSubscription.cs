namespace CustomerMarketing.Core.Domain
{
    public sealed class NeverSubscription : ISubscription
    {
        public SubscriptionMode Mode => SubscriptionMode.Never;
    }
}