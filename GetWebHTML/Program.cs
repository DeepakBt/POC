using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GetWebHTML
{
    class Program
    {
        //static async Task Main(string[] args)
        //{
        //    using var client = new HttpClient();
        //    string HtmlData =  await client.GetStringAsync("https://enetbanking.hdfcbank.com/corporate/CorporateLogin.html");
        //    Console.WriteLine(HtmlData);
        //    Console.ReadKey();
        //}
        static void Main(string[] args)
        {
            CookieContainer cc = new CookieContainer();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://enetbanking.hdfcbank.com/corporate/CorporateLogin.html");
            req.Method = "GET";
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,/;q=0.8";
            req.CookieContainer = cc;
            string responseText;
            try
            {
                // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                var encoding = ASCIIEncoding.ASCII;

                using (var reader = new System.IO.StreamReader(resp.GetResponseStream(), encoding))
                {
                    responseText = reader.ReadToEnd();
                }
                if (resp.Cookies.Count == 0)
                {
                    Console.WriteLine("Unable to aquire Cookies. Status Code is " + resp.StatusCode.ToString(), "HDFC");
                }
                resp.Close();
                Console.WriteLine(responseText);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to aquire Cookies" + ex.ToString(), "HDFC");
            }
            Console.ReadKey();
        }
    }
}
