using System.Collections.Generic;
using CustomerMarketing.Core.Domain;

namespace CustomerMarketing.Core.Readers
{
    public interface IScheduleInputReader
    {
        IEnumerable<Customer> Read(string filePath);
    }
}