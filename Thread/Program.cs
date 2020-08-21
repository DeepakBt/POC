using System;
using System.Threading;
namespace ThreadDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Thread tr = new Thread(Program.Meth1);
            tr.Start();
            Thread tr2 = new Thread(Program.Meth2);
            tr2.Start();
            Thread tr3 = new Thread(Program.Meth3);
            tr3.Start(5);
            Console.WriteLine("Program End");
        }
        public static void Meth1()
        {
            for (int i = 0; i <= 5; i++)
            {
                Console.WriteLine($"i Meth1 value : {i}");
            }
        }
        public static void Meth2()
        {
            for (int i = 0; i <= 5; i++)
            {
                Console.WriteLine($"i Meth2 value : {i}");
            }
        }
        public static void Meth3(object num)
        {
            for (int i = 0; i <= (int)num; i++)
            {
                Console.WriteLine($"i Meth3 value : {i}");
            }
        }
    }
}
