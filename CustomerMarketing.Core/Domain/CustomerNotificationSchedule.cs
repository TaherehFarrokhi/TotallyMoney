using System;

namespace CustomerMarketing.Core.Domain
{
    public sealed class CustomerNotificationSchedule
    {
        public CustomerNotificationSchedule(string customerName, DateTime[] notificationDates)
        {
            CustomerName = customerName ?? throw new ArgumentNullException(nameof(customerName));
            NotificationDates = notificationDates ?? throw new ArgumentNullException(nameof(notificationDates));
        }

        public string CustomerName { get; init; }
        public DateTime[] NotificationDates { get; init; }
    }
}