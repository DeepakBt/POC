using System;
using System.IO;
using System.Linq;

namespace ReadFileNames
{
    class Program
    {
        static void Main(string[] args)
        {
            string strFilePath = @"D:\UbuntuShare\netcoreapp3.1\";
            DirectoryInfo di = new DirectoryInfo(strFilePath);
           
            var Source = Directory.GetFiles(strFilePath).Where(x => new FileInfo(x).CreationTime >= DateTime.Now.AddDays(-3).Date);
            Console.WriteLine(Source.Count());
            Console.ReadLine();
        }
    }
}
