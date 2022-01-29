using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareInfo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            List<MyDriveDetails> HDDList = new List<MyDriveDetails>();
            Console.WriteLine("Drive\tTotalSize\tFreeSize\tFreePercentage");
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    MyDriveDetails ObjHDD = new MyDriveDetails();
                    ObjHDD.Name = drive.Name;
                    ObjHDD.TotalSize = Convert.ToInt64(drive.TotalSize/ Math.Pow(1024, 3));
                    ObjHDD.FreeSize = Convert.ToInt64(drive.TotalFreeSpace / Math.Pow(1024, 3));
                    ObjHDD.FreePercentage = Math.Round((Convert.ToDecimal(ObjHDD.FreeSize) / Convert.ToDecimal(ObjHDD.TotalSize)) * 100,2);
                    HDDList.Add(ObjHDD);
                }
            }
            foreach (MyDriveDetails ObjHDD in HDDList)
            {
                Console.WriteLine($"{ ObjHDD.Name}\t{ObjHDD.TotalSize}\t\t{ObjHDD.FreeSize}\t\t{ObjHDD.FreePercentage}");
            }
            Console.ReadLine();
        }
    }
    public class MyDriveDetails
    {
        public string Name { get; set; }
        public long TotalSize { get; set; }
        public long FreeSize { get; set; }
        public decimal FreePercentage { get; set; }
    }
}
