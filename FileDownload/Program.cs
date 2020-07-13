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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
