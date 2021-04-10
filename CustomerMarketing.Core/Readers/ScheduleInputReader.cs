using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CustomerMarketing.Core.Domain;
using CustomerMarketing.Core.Extensions;
using EnumsNET;

namespace CustomerMarketing.Core.Readers
{
    public class ScheduleInputReader : IScheduleInputReader
    {
        private readonly IFileReader _fileReader;
        private readonly InputSettings _inputSettings;

        private readonly SubscriptionMode[] _modesWithExtraInfo = {SubscriptionMode.Monthly, SubscriptionMode.Weekly};

        public ScheduleInputReader(IFileReader fileReader, InputSettings inputSettings)
        {
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            _inputSettings = inputSettings ?? throw new ArgumentNullException(nameof(inputSettings));
        }

        public IEnumerable<Customer> Read(string filePath)
        {
            if (filePath == null) throw new ArgumentNullException(nameof(filePath));
            
            if (!_fileReader.Exists(filePath))
                throw new FileNotFoundException($"Input file {filePath} in not exists");
            
            var lines = _fileReader.ReadAllLines(filePath);
            var customers = new List<Customer>();
            foreach (var line in lines)
            {
                var columns = line.Split(_inputSettings.ColumnSeparator, StringSplitOptions.RemoveEmptyEntries);
                if (columns.Length < 2)
                    throw new InvalidRowException("Invalid row format. less than 2 fields",line);

                var name = columns[0];
                var mode = Enums.Parse<SubscriptionMode>(columns[1]);
                var extraParams = Array.Empty<string>();
                
                if (_modesWithExtraInfo.Contains(mode) && columns.Length == 2)
                    throw new InvalidRowException($"Invalid row format for {mode.ToString()}. missing extra info",line);
                
                if (_modesWithExtraInfo.Contains(mode))
                    extraParams = columns[2]
                        .Split(_inputSettings.ColumnValueSeparator, StringSplitOptions.RemoveEmptyEntries);

                ISubscription subscription = mode switch
                {
                    SubscriptionMode.Never => new NeverSubscription(),
                    SubscriptionMode.Daily => new DailySubscription(),
                    SubscriptionMode.Weekly => WeeklySubscription.FromValues(extraParams),
                    SubscriptionMode.Monthly =>MonthlySubscription.FromValues(line, extraParams),
                    _ => throw new ArgumentOutOfRangeException()
                };

                customers.Add(new Customer(name, subscription));
            }

            return customers;
        }
    }
}