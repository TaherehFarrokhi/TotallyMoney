using System;
using CustomerMarketing.Core;
using CustomerMarketing.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CustomerMarketing
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            var logger = GetLogger(serviceProvider);
            try
            {
                logger.LogInformation("Processing customer schedules");

                var processor = serviceProvider.GetRequiredService<ICustomerNotificationProcessor>();
                processor.Process("schedule.csv", DateTime.Today, 90);

                logger.LogInformation("Customer schedules process successfully");
            }
            catch (Exception e)
            {
                logger.LogError("Error in processing customer schedules", e);
                Environment.Exit(-1);
            }
        }

        private static ILogger GetLogger(ServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<ILogger<CustomerNotificationProcessor>>();
        }

        private static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddLogging(l => l.AddConsole())
                .AddCustomerMarketingModule()
                .BuildServiceProvider();
        }
    }
}