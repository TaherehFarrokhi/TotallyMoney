namespace CustomerMarketing.Core.Domain
{
    public sealed class Customer
    {
        public Customer(string name, ISubscription subscription)
        {
            Name = name;
            Subscription = subscription;
        }

        public string Name { get; init; }
        public ISubscription Subscription { get; init; }
    }
}