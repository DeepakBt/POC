using System;

namespace Palindrom
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Palindrom");
            string strInput = string.Empty;
            Console.Write("Enter word to check palindrom :");
            strInput = Console.ReadLine();
            string str1 = string.Empty;
            string str2 = string.Empty;
            bool IsPalindrom = true;
            for (int i = 0; i <= strInput.Length-1; i++)
            {
                IsPalindrom = CheckisPaindrom(strInput[i].ToString(), strInput[strInput.Length - (1+i)].ToString());
                if (IsPalindrom == false)
                {
                    break;
                }
            }
            if (IsPalindrom == true)
            {
                Console.Write("Given word is palindrom");
            }
            else
            {
                Console.Write("Given word is not a  palindrom");
            }
            Console.ReadKey();
        }
        public static bool CheckisPaindrom(string str1, string str2)
        {
            if (str1.ToUpper() == str2.ToUpper())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
