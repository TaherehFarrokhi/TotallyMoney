using System;

namespace CustomerMarketing.Core
{
    public interface ICustomerNotificationProcessor
    {
        void Process(string filePath, DateTime startDate, int numberOfDays);
    }
}