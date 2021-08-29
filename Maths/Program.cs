using System;

namespace Maths
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Maths");
            start:
            bool isOdd;
            Console.WriteLine("Please enter number between 1 t o 9");
            int i = Convert.ToInt32(Console.ReadLine());
            int count=0;
            if (i<1 || i > 9)
            {
                Console.WriteLine("Please choose between 1 to 9");
                goto start;
            }
            else
            {
                checkAgain:
                isOdd = Program.isOdd(i);
                if (isOdd)
                {
                    Console.WriteLine("value of i is odd {0}",i);
                    i=i * 3 + 1;
                }
                else
                {
                    Console.WriteLine("value of i is even {0}", i);
                    i = i / 2;
                }
                if(i==1)
                {
                    count = count + 1;
                }
                if(i==1 && count == 10)
                {
                    Console.WriteLine("Entered in loop exit now");
                    goto Exit;
                }
                goto checkAgain;
            }
            Exit:
            Console.ReadKey();
        }
        public static bool isOdd(int num)
        {
            if(num % 2 == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
