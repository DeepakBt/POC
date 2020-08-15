using System;
using System.Collections.Generic;
using System.Text;

namespace StockData
{
    public class TradeData
    {
        public string sym { get; set; }
        public string T { get; set; }
        public float P { get; set; }
        public decimal Q { get; set; }
        public float TS { get; set; }
        public string side { get; set; }
        public long TS2 { get; set; }
    }
}
