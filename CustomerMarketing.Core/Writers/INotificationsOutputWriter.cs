using System;
using System.Collections.Generic;
using CustomerMarketing.Core.Domain;

namespace CustomerMarketing.Core.Writers
{
    public interface INotificationsOutputWriter
    {
        void Write(IEnumerable<CustomerNotificationSchedule> notificationSchedules, DateTime startDate,
            int numberOfDays);
    }
}