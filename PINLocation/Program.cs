using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PINLocation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string origin = "400097";
            string destination = "400069";
            string url = @"https://maps.googleapis.com/maps/api/distancematrix/xml?key=AIzaSyD76RsJFYvlTD2Xxv1o8TqW9Ql_Lv7ph30&origins=" + origin + "&destinations=" + destination + "&sensor=false";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();

           

            Console.ReadLine();
        }
    }
}
