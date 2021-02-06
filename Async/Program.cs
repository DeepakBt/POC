using System;
using System.Threading.Tasks;
namespace Async
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Async Programming!");
            Program ObjP = new Program();
            ObjP.Meth1();
            ObjP.Meth2();

            //ObjP.Meth2();
            //ObjP.Meth1();
            Console.ReadLine();
        }
        public async void Meth1()
        {
            await Task.Run(()=>{
            for(int i=1;i<=20;i++)
                {
                    Console.WriteLine($"{i} Async");
                }
            });
        }
        public void Meth2()
        {
            for (int i = 1; i <= 20; i++)
            {
                Console.WriteLine($"{i} Sync");
            }
        }
    }
}
