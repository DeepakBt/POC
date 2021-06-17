using System;

namespace Recursion
{
    class Program
    {
        static void Main(string[] args)
        {
            MyFun(1);
            Console.ReadLine();
        }
        public static void MyFun(int i)
        {
            if (i <= 100)
            {
                Console.WriteLine(i);
                i += 1;
                MyFun(i);
            }
        }
    }
}
