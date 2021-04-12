using CustomerMarketing.Core.Readers;
using CustomerMarketing.Core.Strategies;
using CustomerMarketing.Core.Writers;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerMarketing.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomerMarketingModule(this IServiceCollection services)
        {
            services.AddSingleton(new InputSettings());
            services.AddSingleton<IFileReader, FileReader>();
            services.AddSingleton<IScheduleInputReader, ScheduleInputReader>();
            services.AddSingleton<INotificationsOutputWriter, DailyNotificationsOutputWriter>();
            services.AddSingleton<ICustomerNotificationProcessor, CustomerNotificationProcessor>();
            services.AddSingleton<INotificationScheduleStrategy, NeverNotificationScheduleStrategy>();
            services.AddSingleton<INotificationScheduleStrategy, DailyNotificationScheduleStrategy>();
            services.AddSingleton<INotificationScheduleStrategy, WeeklyNotificationScheduleStrategy>();
            services.AddSingleton<INotificationScheduleStrategy, MonthlyNotificationScheduleStrategy>();
            return services;
        }
    }
}