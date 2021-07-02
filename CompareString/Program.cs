using System;
using System.Linq;
namespace CompareString
{
    class Program
    {
        static void Main(string[] args)
        {
            string Name1 = string.Empty;
            string Name2 = string.Empty;
            Console.Write("Please Enter First Name :");
            Name1 = Console.ReadLine();
            Console.Write("Please Enter Second Name : ");
            Name2 = Console.ReadLine();
            Console.WriteLine(GetMatchPercentage(Name1, Name2));
            Console.ReadLine();
        }
        public static decimal GetMatchPercentage(string Name1,string Name2)
        {
            decimal Perc = 0.00M;
            int TotalWord=0, MatchWord=0;
            bool IsFirstFlag = true;
            string[] NameOne = Name1.ToUpper().Replace("\t"," ").Split(" ");
            string[] NameTwo = Name2.ToUpper().Replace("\t"," ").Split(" ");
            NameOne = NameOne.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray();
            NameTwo = NameTwo.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray();
            if (NameOne.Length >= NameTwo.Length)
            {
                TotalWord = NameOne.Length;
            }
            else
            {
                TotalWord = NameTwo.Length;
                IsFirstFlag = false;
            }
            if (IsFirstFlag == true)
            {
                for (int i = 0; i <= NameOne.Length - 1; i++)
                {
                    for (int j = 0; j <= NameTwo.Length - 1; j++)
                    {
                        if (NameOne[i] == NameTwo[j])
                        {
                            Console.WriteLine(NameOne[i] + " " + NameTwo[j]);
                            MatchWord = MatchWord + 1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i <= NameTwo.Length - 1; i++)
                {
                    for (int j = 0; j <= NameOne.Length - 1; j++)
                    {
                        if (NameTwo[i] == NameOne[j])
                        {
                            MatchWord = MatchWord + 1;
                        }
                    }
                }
            }
            Perc = (Convert.ToDecimal(MatchWord) / Convert.ToDecimal(TotalWord)) * 100;
            return Math.Round(Perc,2);
        }
    }
}
