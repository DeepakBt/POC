using System;
using System.Net;

namespace FileDownload
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            try
            {
                byte[] result = null;
                using (WebClient webClient = new WebClient())
                {
                    result = webClient.DownloadData("http://midoffice.motilaloswal.com/PMS/UPLOAD/FEEANEXURE/SIDDHARTH KARMARKAR AOP.jpg");
                }

                string data = null;
                data = "mark";
                string data2 =data ?? "jack";
                Console.WriteLine(data2);
                bool d = data is string;
                Console.WriteLine(d);
                Console.WriteLine(d == true ? "Yes" : "No");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
