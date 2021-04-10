namespace CustomerMarketing.Core.Readers
{
    public interface IFileReader
    {
        bool Exists(string filePath);
        string[] ReadAllLines(string filePath);
    }
}