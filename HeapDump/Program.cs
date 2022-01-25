

public class MyDemo
{
    public static void Main()
    {
        Console.WriteLine("Heap Dump");
        Console.ReadLine();
        //var Dump = new HeapClass<int,int,int>();
       // var StartDump = Dump.DumpHeap;
    }
}

public class HeapClass<T1, T2, T3>
{
    public HeapClass<HeapClass<T1,T2,T3>,HeapClass<T1,T2,T3>,HeapClass<T1,T2,T3>>? DumpHeap { get; }
}


