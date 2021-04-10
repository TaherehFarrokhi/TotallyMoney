using System;

namespace CustomerMarketing.Core.Readers
{
    public sealed class InvalidRowException : Exception
    {
        public string Line { get; }

        public InvalidRowException(string message, string line) : base(message)
        {
            Line = line;
        }
    }
}