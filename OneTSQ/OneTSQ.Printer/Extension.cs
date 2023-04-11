using System.IO;

namespace OneTSQ.Printer
{
    public static class Extension
    {
        public static void SaveFilePdf(this byte[] data, string filePath)
        {
            File.WriteAllBytes(filePath, data);
        }
    }
}