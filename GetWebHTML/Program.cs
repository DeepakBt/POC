using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GetWebHTML
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new HttpClient();
            string HtmlData =  await client.GetStringAsync("https://enetbanking.hdfcbank.com/corporate/CorporateLogin.html");
            Console.WriteLine(HtmlData);
            Console.ReadKey();
        }
    }
}
