using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace CustomerMarketing.Core.Readers
{
    [ExcludeFromCodeCoverage]
    public class FileReader : IFileReader
    {
        public bool Exists(string filePath)
        {
            if (filePath == null) throw new ArgumentNullException(nameof(filePath));

            return File.Exists(filePath);
        }

        public string[] ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
}