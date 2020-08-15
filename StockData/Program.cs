using System;
using Newtonsoft;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace StockData
{
    class Program
    {
        static List<string> Symbol = new List<string>();
        static List<float> Price = new List<float>();
        static List<StockPrice> _StockPrice = new List<StockPrice>();
        static void Main(string[] args)
        {
            List<TradeData> LstData = new List<TradeData>();

            //LstData = new Thread(new ThreadStart(LoadData));
            //Thread thread = new Thread(new ThreadStart(LoadData));
            //new Thread(() =>{LstData = LoadData();}).Start();
            //Thread.Sleep(3000);
            LstData = LoadData();
            int count = LstData.Count;
            for(int i =0;i<=count-1;i++)
            {
                FSM(LstData[i]);
                if(_StockPrice.Count>0)
                {
                    Console.Clear();
                    Console.WriteLine("Traders Console Data");
                    Console.WriteLine("*************************************************************************");
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Symbol               Price\n");
                    sb.Append("\n");
                    for (int s = 0;s<=_StockPrice.Count-1;s++)
                    {
                        sb.Append(string.Format("{0,-10}            {1}\n", _StockPrice[s].Symbol, _StockPrice[s].Price));
                    }
                    Console.WriteLine(sb);
                }
                Thread.Sleep(15000);
            }
            Console.ReadKey();
        }
        public static void FSM(TradeData td)
        {
            StockPrice sp = new StockPrice();
            sp.Symbol = td.sym;
            sp.Price = td.P;
            bool Add = true;
            int Count = _StockPrice.Count;
            if (Count > 0)
            {
                for(int i=0;i<= Count-1; i++)
                {
                    if(_StockPrice[i].Symbol.Trim()==sp.Symbol.Trim())
                    {
                        Add = false;
                        _StockPrice[i].Price = sp.Price;
                        break;
                    }
                }
            }else
            {
                Add = false;
                _StockPrice.Add(sp);
            }
            if(Add)
            {
                _StockPrice.Add(sp);
            }
        }
        public static List<TradeData> LoadData()
        {
            try
            {
                List<TradeData> TData = new List<TradeData>();
                string TraderDataPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
                TraderDataPath = TraderDataPath + "\\traderData\\trades.json";
                using (StreamReader file = new StreamReader(TraderDataPath))
                {
                    int counter = 0;
                    string ln;
                    while ((ln = file.ReadLine()) != null)
                    {
                        TradeData SingleData = new TradeData();
                        SingleData = JsonConvert.DeserializeObject<TradeData>(ln);
                        TData.Add(SingleData);
                        counter++;
                    }
                    file.Close();
                }
                return TData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
