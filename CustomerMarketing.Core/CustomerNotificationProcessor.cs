using System;
using System.Collections.Generic;
using System.Linq;
using CustomerMarketing.Core.Domain;
using CustomerMarketing.Core.Readers;
using CustomerMarketing.Core.Strategies;
using CustomerMarketing.Core.Writers;

namespace CustomerMarketing.Core
{
    public sealed class CustomerNotificationProcessor : ICustomerNotificationProcessor
    {
        private readonly IEnumerable<INotificationScheduleStrategy> _notificationScheduleStrategies;
        private readonly IScheduleInputReader _reader;
        private readonly INotificationsOutputWriter _writer;

        public CustomerNotificationProcessor(IScheduleInputReader reader,
            INotificationsOutputWriter writer,
            IEnumerable<INotificationScheduleStrategy> notificationScheduleStrategies)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _notificationScheduleStrategies = notificationScheduleStrategies ??
                                              throw new ArgumentNullException(nameof(notificationScheduleStrategies));
        }

        public void Process(string filePath, DateTime startDate, int numberOfDays)
        {
            var customers = _reader.Read(filePath);

            var schedules = customers.Select(c =>
            {
                var strategy = _notificationScheduleStrategies.FirstOrDefault(x => x.Mode == c.Subscription.Mode);
                if (strategy == null)
                    throw new ScheduleStrategyNotFoundException(c.Subscription.Mode);

                var notificationDates = strategy.CalculateSchedule(c.Subscription, startDate, numberOfDays);
                return new CustomerNotificationSchedule(c.Name, notificationDates.ToArray());
            }).ToArray();

            _writer.Write(schedules, startDate, numberOfDays);
        }
    }
}