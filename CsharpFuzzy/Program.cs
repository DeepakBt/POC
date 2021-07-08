using System;
using System.Net.NetworkInformation;

namespace CsharpFuzzy
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("MAC Address : "+ Convert.ToString(GetMacAddress()));
            HDDInfo();
            Console.ReadLine();
        }
        public static PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet && nic.OperationalStatus == OperationalStatus.Up && nic.Name.ToUpper()== "Ethernet".ToUpper())
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }

        public static void HDDInfo()
        {
            System.Management.ManagementClass mgmtClass = new System.Management.ManagementClass("Win32_Service");
            System.Management.ManagementObjectCollection mgmtObjCol = mgmtClass.GetInstances();

            foreach (System.Management.ManagementObject MGMTOBJ in mgmtObjCol)
            {
                Console.WriteLine(MGMTOBJ.ToString());
                foreach (System.Management.PropertyData property in MGMTOBJ.Properties)
                {
                    Console.WriteLine("Property Name: " + property.Name + "\tProperty Value: " + property.Value);
                }
            }
        }
    }
}
