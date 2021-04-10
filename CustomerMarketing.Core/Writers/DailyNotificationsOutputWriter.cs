using System;
using System.Collections.Generic;
using System.Linq;
using CustomerMarketing.Core.Domain;
using CustomerMarketing.Core.Extensions;

namespace CustomerMarketing.Core.Writers
{
    public sealed class DailyNotificationsOutputWriter : INotificationsOutputWriter
    {
        private const int MaximumDateOutputLength = 21;

        public void Write(IEnumerable<CustomerNotificationSchedule> notificationSchedules, DateTime startDate,
            int numberOfDays)
        {
            var customerSchedules = GetDatesWithCustomerSchedules(notificationSchedules);
            var dateRangeWithCustomerSchedules =
                GenerateDateRangeWithCustomerSchedules(startDate, numberOfDays, customerSchedules);

            GenerateOutput(dateRangeWithCustomerSchedules);
        }

        private static void GenerateOutput(List<(DateTime Date, string? Customers)> dateRangeWithCustomerSchedules)
        {
            dateRangeWithCustomerSchedules
                .ForEach(o =>
                    Console.WriteLine(
                        $"{o.Date.ToString("ddd dd-MMMM-yyyy").PadRight(MaximumDateOutputLength)}\t{o.Customers}"));
        }

        private static List<(DateTime Date, string? Customers)> GenerateDateRangeWithCustomerSchedules(
            DateTime startDate, int numberOfDays, IEnumerable<(DateTime Date, string Customers)> customerSchedules)
        {
            return startDate.ToDateRange(numberOfDays)
                .GroupJoin(customerSchedules,
                    x => x,
                    y => y.Date,
                    (date, dateCustomers) => (Date: date,
                        Customers: dateCustomers.FirstItemPropertyOrDefault(m => m.Customers)))
                .ToList();
        }

        private static IEnumerable<(DateTime Date, string Customers)> GetDatesWithCustomerSchedules(
            IEnumerable<CustomerNotificationSchedule> notificationSchedules)
        {
            return notificationSchedules
                .SelectMany(x => x.NotificationDates,
                    (customerNotificationSchedule, notificationDate) =>
                        (NotificationDate: notificationDate.Date, customerNotificationSchedule.CustomerName))
                .GroupBy(x => x.NotificationDate)
                .Select(x => (Date: x.Key, Customers: string.Join(", ", x.Select(c => c.CustomerName))));
        }
    }
}